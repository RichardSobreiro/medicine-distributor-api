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

        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentRequestDto paymentDto)
        {
            return Created("api/payments", await _paymentsBusiness.CreateNewPayment(paymentDto));
        }

        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _paymentsBusiness.GetPaymentByIdAsync(id));
        }

        [ProducesResponseType(typeof(List<PaymentResponseDto>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("boleto/{userEmail}")]
        public async Task<IActionResult> GetByUserEmail(string userEmail)
        {
            return Ok(await _paymentsBusiness.GetPaymentByUserEmailAsync(userEmail));
        }
    }
}
