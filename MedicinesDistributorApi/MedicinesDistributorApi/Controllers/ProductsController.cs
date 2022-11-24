using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MedicinesDistributorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        IProductsBusiness _productsBusiness;

        public ProductsController(ILogger<ProductsController> logger, IProductsBusiness productsBusiness)
        {
            _logger = logger;
            _productsBusiness = productsBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            return Created("api/products", await _productsBusiness.CreateNewProduct(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto productDto)
        {
            return Ok(await _productsBusiness.UpdateAsync(productDto));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productsBusiness.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? partialName)
        {
            if(string.IsNullOrEmpty(partialName))
                return Ok(await _productsBusiness.GetAll());
            else
                return Ok(await _productsBusiness.GetByName(partialName));
        }
    }
}