using MedicinesDistributorApi.PagSeguro;

namespace MedicinesDistributorApi.Repository.IRepository.IPagSeguro
{
    public interface IPaymentsPagSeguroRepository
    {
        Task<BoletoResponse> CreatePayment(BoletoRequest boletoRequest);
    }
}
