using MedicinesDistributorApi.Dtos.Payments;

namespace MedicinesDistributorApi.Business.IBusiness
{
    public interface IPaymentsBusiness
    {
        Task<PaymentDto> CreateNewPayment(PaymentDto paymentDto);
    }
}
