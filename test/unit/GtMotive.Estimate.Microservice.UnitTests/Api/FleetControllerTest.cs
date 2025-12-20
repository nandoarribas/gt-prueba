using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command;
using GtMotive.Estimate.Microservice.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Api
{
    public class FleetControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly FleetController _sut;
        private readonly Mock<RentVehicleCommand> _rentCommandMock;
        private readonly string _vehicleId = "V100";
        private readonly string _clientId = "C200";

        public FleetControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new FleetController(_mediatorMock.Object);

            _rentCommandMock = new Mock<RentVehicleCommand>(_vehicleId, _clientId);
        }

        [Fact]
        public async Task Rent_ShouldReturnNoContent_WhenManagerSucceeds()
        {
            // Act
            var result = await _sut.Rent(_rentCommandMock.Object);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mediatorMock.Verify(m => m.Send(It.Is<RentVehicleCommand>(cmd => cmd.VehicleId == _vehicleId && cmd.ClientId == _clientId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Rent_ShouldReturnError_WhenDomainExceptionIsThrown()
        {
            // Arrange
            var errorMessage = "El vehículo ya está alquilado.";
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RentVehicleCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DomainException(errorMessage));

            // Act
            var exceptionResult = await Assert.ThrowsAsync<DomainException>(async () => await _sut.Rent(_rentCommandMock.Object));

            // Assert
            Assert.Equal(errorMessage, exceptionResult.Message);
        }
    }
}
