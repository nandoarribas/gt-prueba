using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
#pragma warning disable xUnit1000 // Test classes must be public
    public sealed class VehicleRepositoryTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
#pragma warning restore xUnit1000 // Test classes must be public
    {
        [Fact]
        public async Task CreateAsync_ShouldPersistVehicleInDatabase()
        {
            // Arrange
            using var scope = Fixture.Server.Services.CreateScope();
            var (context, sut) = await ClearInfrastructure(scope);
            var vehicle = new Vehicle { Id = "V99", FabricationYear = 2022 };

            // Act
            await sut.CreateAsync(vehicle);

            // Assert
            var dbVehicle = await context.Vehicles.FindAsync("V99");
            dbVehicle.Should().NotBeNull();
            dbVehicle.FabricationYear.Should().Be(2022);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenVehicleDoesNotExist()
        {
            // Arrange
            using var scope = Fixture.Server.Services.CreateScope();
            var (_, sut) = await ClearInfrastructure(scope);

            // Act
            var result = await sut.GetByIdAsync("NOT-EXISTS");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldPersistInDatabase()
        {
            // Arrange
            using var scope = Fixture.Server.Services.CreateScope();
            var (context, sut) = await ClearInfrastructure(scope);
            var existingVehicle = await SeedVehicle(context);

            // Act
            await sut.UpdateAsync(existingVehicle);

            // Assert
            var dbVehicle = await context.Vehicles.FindAsync("V99");
            dbVehicle.CurrentClientID.Should().Be("C123");
        }

        private static async Task<Vehicle> SeedVehicle(VehicleDbContext context)
        {
            var existingVehicle = new Vehicle { Id = "V99", FabricationYear = 2024, CurrentClientID = "C123" };
            context.Vehicles.Add(existingVehicle);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            return existingVehicle;
        }

        private static async Task<(VehicleDbContext _context, IVehicleRepository _sut)> ClearInfrastructure(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<VehicleDbContext>();
            var sut = scope.ServiceProvider.GetRequiredService<IVehicleRepository>();

            context.Vehicles.RemoveRange(context.Vehicles);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            return (context, sut);
        }
    }
}
