using System;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.Data;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    public sealed class RentVehicleSpecs(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task Rent_Vehicle_Should_Update_Database_Correctly()
        {
            var vehicleId = "V99";
            var clientId = "C123";

            // Arrange
            await Seed(vehicleId);
            var command = new RentVehicleCommand(vehicleId, clientId);

            // Act
            await Fixture.UsingHandlerForRequest<RentVehicleCommand>(async handler =>
            {
                await handler.Handle(command, default);
            });

            // Assert
            await Fixture.UsingRepository<VehicleDbContext>(async context =>
            {
                var dbVehicle = await context.Vehicles.FindAsync(vehicleId);
                dbVehicle.Should().NotBeNull();
                dbVehicle.CurrentClientID.Should().Be(clientId);
            });
        }

        private async Task Seed(string vehicleId)
        {
            await Fixture.UsingRepository<VehicleDbContext>(async context =>
            {
                context.Vehicles.RemoveRange(context.Vehicles);
                context.Vehicles.Add(new Vehicle
                {
                    Id = vehicleId,
                    FabricationYear = DateTime.Now.Year,
                    CurrentClientID = null
                });
                await context.SaveChangesAsync();
            });
        }
    }
}
