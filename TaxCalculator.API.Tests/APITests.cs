using TaxCalculator.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaxCalculator.API.Controllers;
using TaxCalculator.Application.Interfaces;

namespace TaxCalculator.API.Tests
{
    [TestClass]
    public class VatControllerTests
    {
        private Mock<IVatCalculationService> _mockVatCalculationService;
        private VatController _vatController;

        [TestInitialize]
        public void Setup()
        {
            // Criar o mock do serviço de cálculo de VAT
            _mockVatCalculationService = new Mock<IVatCalculationService>();

            // Instanciar o controlador com a dependência mockada
            _vatController = new VatController(_mockVatCalculationService.Object);
        }

        [TestMethod]
        public void CalculateVat_WithValidNetAmount_ReturnsCorrectCalculation()
        {
            // Arrange
            var request = new VatCalculationRequest
            {
                Net = 100.0m,
                VatRate = 0.2m // Assuming a 20% VAT rate (Austria)
            };

            var expectedResponse = new VatCalculationResponse
            {
                Net = 100.0m,
                Gross = 120.0m, // 100 + 20% VAT
                Vat = 20.0m // 20% VAT
            };

            // Configurar o mock para retornar a resposta esperada
            _mockVatCalculationService
                .Setup(service => service.CalculateVat(It.IsAny<VatCalculationRequest>()))
                .Returns(expectedResponse);

            // Act
            var result = _vatController.CalculateVat(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>(); // Verifica se é um OK
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(expectedResponse); // Verifica se o valor retornado é o esperado
        }

        [TestMethod]
        public void CalculateVat_WithInvalidVatRate_ReturnsBadRequest()
        {
            // Arrange
            var request = new VatCalculationRequest
            {
                Net = 100.0m,
                VatRate = 0.5m // Invalid VAT rate (must be 10%, 13%, or 20%)
            };

            // Act
            var result = _vatController.CalculateVat(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.ToString().Should().Contain("Invalid VAT rate input.");
        }
    }
}
