using TaxCalculator.Application.DTOs;

namespace TaxCalculator.Application.Interfaces
{
    public interface IVatCalculationService
    {
        VatCalculationResponse CalculateVat(VatCalculationRequest request);
    }
}
