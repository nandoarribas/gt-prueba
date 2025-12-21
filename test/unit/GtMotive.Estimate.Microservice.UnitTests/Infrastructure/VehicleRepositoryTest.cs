using System;
using System.Threading.Tasks;
using FluentAssertions;
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
        public async Task CreateAsync_ShouldThrowException_IfInvalidData()
        {
            // Arrange
            using var context = new VehicleDbContext(_options);
            var sut = new VehicleRepository(context);

            // Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.CreateAsync(null));

            // Assert
            result.ParamName.Should().Be("vehicle");
        }
    }
}
