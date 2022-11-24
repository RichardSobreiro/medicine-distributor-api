using MedicinesDistributorApi.Dtos;

namespace MedicinesDistributorApi.Business.IBusiness
{
    public interface IProductsBusiness
    {
        Task<ProductDto> CreateNewProduct(ProductDto productDto);
        Task<ProductDto> UpdateAsync(ProductDto productDto);
        Task<List<ProductDto>> GetAll();
        Task<List<ProductDto>> GetByName(string partialName);
        Task<ProductDto?> GetById(string id);
        Task RemoveAsync(string id);
    }
}
