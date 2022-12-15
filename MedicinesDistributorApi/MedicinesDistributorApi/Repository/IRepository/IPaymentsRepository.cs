using MedicinesDistributorApi.Models.Payments;

namespace MedicinesDistributorApi.Repository.IRepository
{
    public interface IPaymentsRepository
    {
        Task CreateAsync(Payment product);
        Task<Payment?> GetAsync(string id);
        Task<List<Payment>> GetByEmailAsync(string email);
        Task<List<Payment>> GetAsync();
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Payment product);
    }
}
