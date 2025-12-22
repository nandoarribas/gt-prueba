using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
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
        public async Task Rent_ShouldReturnError_WhenDomainExceptionIsThrown()
        {
            // Arrange
            var errorMessage = "Vehicle already rented.";
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RentVehicleCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DomainException(errorMessage));

            // Act
            var exceptionResult = await Assert.ThrowsAsync<DomainException>(async () => await _sut.Rent(_rentCommandMock.Object));

            // Assert
            Assert.Equal(errorMessage, exceptionResult.Message);
        }

        [Fact]
        public async Task Rent_ShouldReturnOk_WhenCommandSucceeds()
        {
            // Arrange
            var command = new RentVehicleCommand("V1", "C1");
            var expectedDto = new VehicleDto("V1", "C1", true, "Success");

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedDto);

            // Act
            var result = await _sut.Rent(command);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<VehicleDto>().Subject;

            returnedDto.VehicleId.Should().Be("V1");
            returnedDto.Message.Should().Be("Success");
        }
    }
}
