using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    public class RentingManagerTest
    {
        private readonly Mock<IVehicleRepository> _repositoryMock;
        private readonly Mock<IAppLogger<RentingManager>> _loggerMock;
        private readonly Mock<ITelemetry> _telemetryMock;
        private readonly RentingManager _sut;

        public RentingManagerTest()
        {
            _repositoryMock = new Mock<IVehicleRepository>();
            _loggerMock = new Mock<IAppLogger<RentingManager>>();
            _telemetryMock = new Mock<ITelemetry>();

            _sut = new RentingManager(_repositoryMock.Object, _telemetryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task RentVehicle_ShouldThrowException_WhenClientAlreadyHasActiveRental()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.HasActiveRentAsync("client456")).ReturnsAsync(true);

            // Act
            var exceptionResult = await Assert.ThrowsAsync<DomainException>(async () =>
                await _sut.RentVehicle("vehicle123", "client456"));

            // Assert
            Assert.Equal($"The person client456 already has an active rental.", exceptionResult.Message);
            _telemetryMock.Verify(t => t.TrackEvent("RentVehicleFailed", It.IsAny<IDictionary<string, string>>(), It.IsAny<IDictionary<string, double>>()), Times.Once);
        }

        [Fact]
        public async Task RentVehicle_ShouldThrowException_WhenVehicleDoesNotExists()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.HasActiveRentAsync("client456")).ReturnsAsync(false);
            _repositoryMock
                .Setup(r => r.GetByIdAsync("vehicle123")).ReturnsAsync((Vehicle)null);

            // Act
            var exceptionResult = await Assert.ThrowsAsync<DomainException>(async () =>
                 await _sut.RentVehicle("vehicle123", "client456"));

            // Assert
            Assert.Equal($"The requested vehicle does not exist.", exceptionResult.Message);
            _telemetryMock.Verify(t => t.TrackEvent("RentVehicleFailed", It.IsAny<IDictionary<string, string>>(), It.IsAny<IDictionary<string, double>>()), Times.Once);
        }

        [Fact]
        public async Task RentVehicle_ShouldUpdateVehicle_IfValidData()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.HasActiveRentAsync("client456")).ReturnsAsync(false);
            _repositoryMock
                .Setup(r => r.GetByIdAsync("vehicle123")).ReturnsAsync(CreateValidVehicle("V1"));

            // Act
            await _sut.RentVehicle("vehicle123", "client456");

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Vehicle>(v => v.CurrentClientID == "client456" && v.Id == "V1")), Times.Once);
            _telemetryMock.Verify(t => t.TrackEvent("RentVehicleSuccess", It.IsAny<IDictionary<string, string>>(), It.IsAny<IDictionary<string, double>>()), Times.Once);
        }

        private static Vehicle CreateValidVehicle(string id) => new()
        {
            Id = id,
            FabricationYear = DateTime.Now.Year,
            CurrentClientID = null
        };
    }
}
