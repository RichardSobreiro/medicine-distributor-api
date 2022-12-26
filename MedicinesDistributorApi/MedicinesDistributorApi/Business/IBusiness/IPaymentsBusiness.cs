using MedicinesDistributorApi.Dtos.Payments;

namespace MedicinesDistributorApi.Business.IBusiness
{
    public interface IPaymentsBusiness
    {
        Task<PaymentResponseDto> CreateNewPayment(PaymentRequestDto paymentDto);
        Task<PaymentResponseDto> GetPaymentByIdAsync(string id);
        Task<List<PaymentResponseDto>> GetPaymentByUserEmailAsync(string userEmail);
    }
}
