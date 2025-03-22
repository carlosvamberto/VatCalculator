namespace TaxCalculator.Domain.Entities
{
    public class VatInformation
    {
        // Properties are private set to avoid modification after object creation
        public decimal? Net { get; private set; }
        public decimal? Gross { get; private set; }
        public decimal? Vat { get; private set; }
        public decimal VatRate { get; private set; }

        public VatInformation(decimal? net, decimal? gross, decimal? vat, decimal vatRate)
        {
            // Verfy if all values are null
            if (net == null && gross == null && vat == null)
                throw new ArgumentException("At least one value (Net, Gross, VAT) must be provided.");
                        
            // If the API receives one of the net, gross or VAT amounts and additionally a valid
            // Austrian VAT rate(10 %, 13 %, 20 %), the other two missing amounts
            // (net / gross / VAT) are calculated by the system and returned to the client in a
            // meaningful structure
            if (vatRate <= 0 || (vatRate != 10 && vatRate != 13 && vatRate != 20))
                throw new ArgumentException("Invalid VAT rate. Valid values: 10%, 13%, 20%.");

            Net = net;
            Gross = gross;
            Vat = vat;
            VatRate = vatRate / 100m;

            CalculateMissingValues();
        }

        private void CalculateMissingValues()
        {
            if (Net.HasValue)
            {
                Vat = Net * VatRate;
                Gross = Net + Vat;
            }
            else if (Gross.HasValue)
            {
                Net = Gross / (1 + VatRate);
                Vat = Gross - Net;
            }
            else if (Vat.HasValue)
            {
                Net = Vat / VatRate;
                Gross = Net + Vat;
            }
        }
    }
}
