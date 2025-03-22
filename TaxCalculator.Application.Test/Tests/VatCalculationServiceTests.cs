using TaxCalculator.Application.DTOs;
using TaxCalculator.Application.Interfaces;
using TaxCalculator.Application.Services;

namespace TaxCalculator.Application.Test.Tests
{
    [TestClass]
    public class VatCalculationServiceTests
    {
        private IVatCalculationService _vatCalculationService;

        [TestInitialize]
        public void Setup()
        {
            _vatCalculationService = new VatCalculationService();
        }

        [TestMethod]
        public void CalculateVat_WithNetAmount_ShouldCalculateGrossAndVat()
        {
            // Arrange
            var request = new VatCalculationRequest { Net = 100, VatRate = 20 };

            // Act
            var result = _vatCalculationService.CalculateVat(request);

            // Assert
            Assert.AreEqual(100, result.Net);
            Assert.AreEqual(120, result.Gross);
            Assert.AreEqual(20, result.Vat);
        }


        [TestMethod]
        public void CalculateVat_WithGrossAmount_ShouldCalculateNetAndVat()
        {
            var request = new VatCalculationRequest { Gross = 120, VatRate = 20 };

            var result = _vatCalculationService.CalculateVat(request);

            Assert.AreEqual(100, result.Net);
            Assert.AreEqual(120, result.Gross);
            Assert.AreEqual(20, result.Vat);
        }

        [TestMethod]
        public void CalculateVat_WithVatAmount_ShouldCalculateNetAndGross()
        {
            var request = new VatCalculationRequest { Vat = 20, VatRate = 20 };

            var result = _vatCalculationService.CalculateVat(request);

            Assert.AreEqual(100, result.Net);
            Assert.AreEqual(120, result.Gross);
            Assert.AreEqual(20, result.Vat);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateVat_WithInvalidVatRate_ShouldThrowException()
        {
            var request = new VatCalculationRequest { Net = 100, VatRate = 5 }; // invalida VatRate

            _vatCalculationService.CalculateVat(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateVat_WithMoreThanOneAmount_ShouldThrowException()
        {
            var request = new VatCalculationRequest { Net = 100, Gross = 120, VatRate = 20 };

            _vatCalculationService.CalculateVat(request);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateVat_WithNoAmount_ShouldThrowException()
        {
            var request = new VatCalculationRequest { VatRate = 20 };

            _vatCalculationService.CalculateVat(request);
        }
    }
}
