using AutoMapper;
using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Constants;
using MedicinesDistributorApi.Dtos.Payments;
using MedicinesDistributorApi.Models.Payments;
using MedicinesDistributorApi.PagSeguro;
using MedicinesDistributorApi.Repository.IRepository;
using MedicinesDistributorApi.Repository.IRepository.IPagSeguro;

namespace MedicinesDistributorApi.Business
{
    public class PaymentsBusiness : IPaymentsBusiness
    {
        private readonly IMapper _mapper;
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IConfiguration _configuration;
        private readonly IPaymentsPagSeguroRepository _paymentsPagSeguroRepository;
        private readonly IMeasurementUnitsRepository _measurementUnitsRepository;

        public PaymentsBusiness(IConfiguration configuration, IMapper mapper, IPaymentsRepository paymentsRepository,
            IPaymentsPagSeguroRepository paymentsPagSeguroRepository, IMeasurementUnitsRepository measurementUnitsRepository)
        {
            _mapper = mapper;
            _paymentsRepository = paymentsRepository;
            _configuration = configuration;
            _paymentsPagSeguroRepository = paymentsPagSeguroRepository;
            _measurementUnitsRepository = measurementUnitsRepository;   
        }

        public async Task<PaymentResponseDto> CreateNewPayment(PaymentRequestDto paymentDto)
        {
            try
            {
                var payment = _mapper.Map<PaymentRequestDto, Payment>(paymentDto);
                payment.Status = PaymentStatus.PROCESSING;
                await _paymentsRepository.CreateAsync(payment);
                BoletoRequest boletoRequest = CreateBoletoRequest(payment);
                BoletoResponse boletoResponse = await _paymentsPagSeguroRepository.CreatePayment(boletoRequest);
                payment.BoletoResponse = boletoResponse;
                payment.Status = GetPaymentStatusFromBoletoResponse(boletoResponse);
                await _paymentsRepository.UpdateAsync(payment.Id, payment);
                var paymentResponseDto = _mapper.Map<Payment, PaymentResponseDto>(payment);
                if(payment.Status != PaymentStatus.ERROR)
                {
                    BoletoResponse.Link? link = payment.BoletoResponse.links.FirstOrDefault(l => l.media == "application/pdf");
                    if(link != null)
                    {
                        paymentResponseDto.BoletoResponse.Boleto.link = link.href;
                        paymentResponseDto.BoletoResponse.Boleto.barcode = payment.BoletoResponse.payment_method.boleto.barcode;
                        paymentResponseDto.BoletoResponse.Boleto.id = payment.BoletoResponse.payment_method.boleto.id;
                        paymentResponseDto.BoletoResponse.Boleto.formatted_barcode = payment.BoletoResponse.payment_method.boleto.formatted_barcode;
                    }
                }
                else
                {
                    throw new Exception("Erro ao gerar o boleto. Tente novamente.");
                }
                return paymentResponseDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        BoletoRequest CreateBoletoRequest(Payment payment)
        {
            BoletoRequest boletoRequest = new BoletoRequest();
            boletoRequest.reference_id = payment.Id;
            boletoRequest.description = "Compra de Materiais/Medicamentos";
            boletoRequest.amount = new BoletoRequest.Amount()
            {
                value = (int)payment.ShoppingCart.ProductsInCart.Sum(p => 
                    p.Concentrations.Any() ? p.Concentrations.Sum(c => c.SellingPrice * c.Quantity) : p.SellingPrice * p.Quantity),
                currency = "BRL"
            };
            boletoRequest.payment_method = new BoletoRequest.PaymentMethod()
            {
                type = "BOLETO",
                boleto = new BoletoRequest.Boleto()
                {
                    due_date = payment.DueDate.Date.ToString("yyyy-MM-dd"),
                    instruction_lines = new BoletoRequest.InstructionLines()
                    {
                        line_1 = "Pagamento processado para DESC Fatura",
                        line_2 = "Via PagSeguro"
                    },
                    holder = new BoletoRequest.Holder()
                    {
                        name = payment.Name,
                        tax_id = payment.CpfCnpj,
                        email = payment.Email,
                        address = new BoletoRequest.Address()
                        {
                            street = payment.BillingAddress.Street,
                            number = payment.BillingAddress.Number,
                            locality = payment.BillingAddress.District,
                            city = payment.BillingAddress.City,
                            region = payment.BillingAddress.State,
                            region_code = payment.BillingAddress.State,
                            country = "Brasil",
                            postal_code = payment.BillingAddress.Cep
                        }
                    }
                }
            };
            boletoRequest.notification_urls = new List<string>() {
                _configuration["NotificationUrlBoleto"]
            };
            return boletoRequest;
        }

        string GetPaymentStatusFromBoletoResponse(BoletoResponse boletoResponse)
        {
            if(boletoResponse.status.Contains("WAITING", StringComparison.InvariantCultureIgnoreCase))
            {
                return PaymentStatus.WAITING;
            }
            else
            {
                return PaymentStatus.ERROR;
            }
        }

        public async Task<PaymentResponseDto> GetPaymentByIdAsync(string id)
        {
            try
            {
                var payment = await _paymentsRepository.GetAsync(id);
                if(payment != null)
                {
                    foreach (var product in payment.ShoppingCart.ProductsInCart)
                    {
                        if(product.Concentrations.Any())
                        {
                            var measurementUnit = await _measurementUnitsRepository.GetAsync(product.MeasurementUnitId);
                            if (measurementUnit != null)
                            {
                                foreach (var concentration in product.Concentrations)
                                {
                                    concentration.ConcentrationDescription =
                                        $"{concentration.ConcentrationValue} {measurementUnit.MeasurementUnitDescription}";
                                }
                            }
                        }

                    }
                    var paymentResponseDto = _mapper.Map<Payment, PaymentResponseDto>(payment);
                    paymentResponseDto.BoletoResponse.Boleto.link = payment.BoletoResponse.links.FirstOrDefault(l => l.media == "application/pdf")?.href;
                    paymentResponseDto.BoletoResponse.Boleto.barcode = payment.BoletoResponse.payment_method.boleto.barcode;
                    paymentResponseDto.BoletoResponse.Boleto.id = payment.BoletoResponse.payment_method.boleto.id;
                    paymentResponseDto.BoletoResponse.Boleto.formatted_barcode = payment.BoletoResponse.payment_method.boleto.formatted_barcode;
                    return paymentResponseDto;
                }
                else
                {
                    throw new Exception("Pagamento não encontrato");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PaymentResponseDto>> GetPaymentByUserEmailAsync(string userEmail)
        {
            try
            {
                var payments = await _paymentsRepository.GetByEmailAsync(userEmail);
                if (payments != null && payments.Any())
                {
                    List<PaymentResponseDto> result = new List<PaymentResponseDto>();
                    foreach(var payment in payments)
                    {
                        foreach (var product in payment.ShoppingCart.ProductsInCart)
                        {
                            if (product.Concentrations.Any())
                            {
                                var measurementUnit = await _measurementUnitsRepository.GetAsync(product.MeasurementUnitId);
                                if (measurementUnit != null)
                                {
                                    foreach (var concentration in product.Concentrations)
                                    {
                                        concentration.ConcentrationDescription =
                                            $"{concentration.ConcentrationValue} {measurementUnit.MeasurementUnitDescription}";
                                    }
                                }
                            }
                        }
                        var paymentResponseDto = _mapper.Map<Payment, PaymentResponseDto>(payment);
                        paymentResponseDto.BoletoResponse.Boleto.link = payment.BoletoResponse.links.FirstOrDefault(l => l.media == "application/pdf")?.href;
                        paymentResponseDto.BoletoResponse.Boleto.barcode = payment.BoletoResponse.payment_method.boleto.barcode;
                        paymentResponseDto.BoletoResponse.Boleto.id = payment.BoletoResponse.payment_method.boleto.id;
                        paymentResponseDto.BoletoResponse.Boleto.formatted_barcode = payment.BoletoResponse.payment_method.boleto.formatted_barcode;
                        result.Add(paymentResponseDto);
                    }
                    return result;
                }
                else
                {
                    throw new Exception("Pagamento não encontrato");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
