using TaxCalculator.Application.DTOs;
using TaxCalculator.Application.Interfaces;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Application.Services
{
    public class VatCalculationService : IVatCalculationService
    {
        /// <summary>
        /// Calculate the VAT based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return Net, Gross and VAT values</returns>
        /// <exception cref="ArgumentException"></exception>
        public VatCalculationResponse CalculateVat(VatCalculationRequest request)
        {

            // Check how many values ​​were entered
            int providedValuesCount = (request.Net.HasValue ? 1 : 0) + (request.Gross.HasValue ? 1 : 0) + (request.Vat.HasValue ? 1 : 0);

            if (providedValuesCount != 1)
                throw new ArgumentException("Only one amount (Net, Gross, VAT) should be provided.");

            try
            {
                var calculation = new VatInformation(request.Net, request.Gross, request.Vat, request.VatRate);
                return new VatCalculationResponse
                {
                    Net = calculation.Net.Value,
                    Gross = calculation.Gross.Value,
                    Vat = calculation.Vat.Value
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
