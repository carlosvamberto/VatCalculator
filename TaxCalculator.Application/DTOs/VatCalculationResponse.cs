namespace TaxCalculator.Application.DTOs
{
    /// <summary>
    /// Class used to represent the response for the Valt calculation.
    /// </summary>
    public class VatCalculationResponse
    {
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal Vat { get; set; }
    }
}
