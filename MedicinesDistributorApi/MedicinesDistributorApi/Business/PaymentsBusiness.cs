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

        public PaymentsBusiness(IConfiguration configuration, IMapper mapper, IPaymentsRepository paymentsRepository,
            IPaymentsPagSeguroRepository paymentsPagSeguroRepository)
        {
            _mapper = mapper;
            _paymentsRepository = paymentsRepository;
            _configuration = configuration;
            _paymentsPagSeguroRepository = paymentsPagSeguroRepository;
        }

        public async Task<PaymentDto> CreateNewPayment(PaymentDto paymentDto)
        {
            try
            {
                var payment = _mapper.Map<PaymentDto, Payment>(paymentDto);
                payment.Status = PaymentStatus.PROCESSING;
                await _paymentsRepository.CreateAsync(payment);
                BoletoRequest boletoRequest = CreateBoletoRequest(payment);
                BoletoResponse boletoResponse = await _paymentsPagSeguroRepository.CreatePayment(boletoRequest);
                payment.BoletoResponse = boletoResponse;
                payment.Status = GetPaymentStatus(boletoResponse);
                await _paymentsRepository.UpdateAsync(payment.Id, payment);
                var paymentResponseDto = _mapper.Map<Payment, PaymentDto>(payment);
                BoletoResponse.Link? link = payment.BoletoResponse.links.FirstOrDefault(l => l.media == "application/pdf");
                if(link != null)
                {
                    paymentResponseDto.BoletoResponse.Boleto.link = link.href;
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
                value = payment.ShoppingCart.ProductsInCart.Sum(p => p.Concentrations.Sum(c => c.SellingPrice)),
                currency = "BRL"
            };
            boletoRequest.payment_method = new BoletoRequest.PaymentMethod()
            {
                type = "BOLETO",
                boleto = new BoletoRequest.Boleto()
                {
                    due_date = payment.DueDate,
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

        string GetPaymentStatus(BoletoResponse boletoResponse)
        {
            if(boletoResponse.status.Contains("WAITING", StringComparison.InvariantCultureIgnoreCase))
            {
                return PaymentStatus.WAITING;
            }
            else
            {
                return PaymentStatus.WAITING;
            }
        }
    }
}
