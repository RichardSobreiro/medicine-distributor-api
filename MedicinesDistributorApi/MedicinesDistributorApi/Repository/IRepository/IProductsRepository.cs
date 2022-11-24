using MedicinesDistributorApi.Models;

namespace MedicinesDistributorApi.Repository.IRepository
{
    public interface IProductsRepository
    {
        Task CreateAsync(Product product);
        Task<Product?> GetAsync(string id);
        Task<List<Product>> GetByNameAsync(string name);
        Task<List<Product>> GetAsync();
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Product product);
    }
}
