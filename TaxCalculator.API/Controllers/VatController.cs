using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.DTOs;
using TaxCalculator.Application.Interfaces;

namespace TaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatController : ControllerBase
    {
        // Using dependency injection to inject the IVatCalculationService
        private readonly IVatCalculationService _vatCalculationService;

        public VatController(IVatCalculationService vatCalculationService)
        {
            _vatCalculationService = vatCalculationService;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateVat([FromBody] VatCalculationRequest request)
        {
            try
            {
                // Check if the VAT rate is valid (10%, 13%, or 20%)
                if (request.VatRate != 0.1m && request.VatRate != 0.13m && request.VatRate != 0.2m)
                {
                    return BadRequest(new { message = "Invalid VAT rate input. The rate must be 10%, 13%, or 20%." });
                }

                var result = _vatCalculationService.CalculateVat(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
