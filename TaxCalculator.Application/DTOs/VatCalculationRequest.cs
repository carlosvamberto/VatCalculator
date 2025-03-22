﻿namespace TaxCalculator.Application.DTOs
{
    /// <summary>
    /// Class used to represent the request for the Valt calculation.
    /// </summary>
    public class VatCalculationRequest
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Vat { get; set; }
        public decimal VatRate { get; set; }
    }
}
