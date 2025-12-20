using System;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.Data;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Infrastructure
{
    public class VehicleRepositoryTest
    {
        private readonly DbContextOptions<VehicleDbContext> _options;

        public VehicleRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<VehicleDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVehicle_IfVehicleExists()
        {
            // Arrange
            var vehicleId = "V99";
            var expectedVehicle = new Vehicle { Id = vehicleId, FabricationYear = 2023 };

            using (var context = new VehicleDbContext(_options))
            {
                context.Vehicles.Add(expectedVehicle);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new VehicleDbContext(_options))
            {
                var sut = new VehicleRepository(context);
                var result = await sut.GetByIdAsync(vehicleId);

                // Assert
                result.Should().NotBeNull();
                result.Id.Should().Be(vehicleId);
                result.FabricationYear.Should().Be(2023);
                result.CurrentClientID.Should().BeNull();
            }
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_IfInvalidData()
        {
            // Arrange
            var vehicleId = "V99";
            var expectedVehicle = new Vehicle { Id = vehicleId, FabricationYear = 2023 };

            using var context = new VehicleDbContext(_options);
            var sut = new VehicleRepository(context);

            // Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.CreateAsync(null));

            // Assert
            result.ParamName.Should().Be("vehicle");
            context.Vehicles.Should().NotContain(v => v.Id == vehicleId);
        }

        [Fact]
        public async Task CreateAsync_DataOk_ShouldCreateVehicle()
        {
            // Arrange
            var vehicleId = "V99";
            var expectedVehicle = new Vehicle { Id = vehicleId, FabricationYear = 2023 };

            using var context = new VehicleDbContext(_options);
            var sut = new VehicleRepository(context);

            // Act
            await sut.CreateAsync(expectedVehicle);

            // Assert
            context.Vehicles.Should().Contain(v => v.Id == vehicleId);
        }
    }
}
