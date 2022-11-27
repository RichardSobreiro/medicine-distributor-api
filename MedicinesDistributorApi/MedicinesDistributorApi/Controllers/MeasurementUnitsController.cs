using MedicinesDistributorApi.Business.IBusiness;
using Microsoft.AspNetCore.Mvc;

namespace MedicinesDistributorApi.Controllers
{
    [ApiController]
    [Route("api/measurement-units")]
    public class MeasurementUnitsController : ControllerBase
    {
        private readonly ILogger<MeasurementUnitsController> _logger;
        IMeasurementUnitsBusiness _measurementUnitsBusiness;

        public MeasurementUnitsController(ILogger<MeasurementUnitsController> logger, 
            IMeasurementUnitsBusiness measurementUnitsBusiness)
        {
            _logger = logger;
            _measurementUnitsBusiness = measurementUnitsBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _measurementUnitsBusiness.GetAll());
        }
    }
}
