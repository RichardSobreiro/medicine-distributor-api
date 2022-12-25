using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Dtos.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicinesDistributorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        IPaymentsBusiness _paymentsBusiness;

        public PaymentsController(ILogger<ProductsController> logger, IPaymentsBusiness paymentsBusiness)
        {
            _logger = logger;
            _paymentsBusiness = paymentsBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentDto paymentDto)
        {
            return Created("api/payments", await _paymentsBusiness.CreateNewPayment(paymentDto));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _paymentsBusiness.Get(id));
        }
    }
}
