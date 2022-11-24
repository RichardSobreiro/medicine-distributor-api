using AutoMapper;
using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Dtos;
using MedicinesDistributorApi.Models;
using MedicinesDistributorApi.Repository.IRepository;

namespace MedicinesDistributorApi.Business
{
    public class ProductsBusiness : IProductsBusiness
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;

        public ProductsBusiness(IMapper mapper, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        public async Task<ProductDto> CreateNewProduct(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<ProductDto, Product>(productDto);
                product.CreationDate = DateTime.Now;
                product.LastUpdated = product.CreationDate;
                await _productsRepository.CreateAsync(product);
                return _mapper.Map<Product, ProductDto>(product); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> UpdateAsync(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<ProductDto, Product>(productDto);
                product.LastUpdated = DateTime.Now;
                await _productsRepository.UpdateAsync(productDto.Id,  product);
                return _mapper.Map<Product, ProductDto>(product); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto?> GetById(string id)
        {
            try
            {
                var product = await _productsRepository.GetAsync(id);
                if (product != null)
                    return _mapper.Map<Product, ProductDto>(product);
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductDto>> GetByName(string partialName)
        {
            try
            {
                var products = await _productsRepository.GetByNameAsync(partialName);
                return _mapper.Map<List<Product>, List<ProductDto>>(products); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductDto>> GetAll()
        {
            try
            {
                var products = await _productsRepository.GetAsync();
                return _mapper.Map<List<Product>, List<ProductDto>>(products); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RemoveAsync(string id)
        {
            try
            {
                await _productsRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
