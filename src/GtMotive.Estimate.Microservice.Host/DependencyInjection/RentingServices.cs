using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Data;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Host.DependencyInjection
{
    internal static class RentingServices
    {
        public static IServiceCollection AddRentingServices(this IServiceCollection services)
        {
            services.AddDbContext<VehicleDbContext>(options =>
                options.UseInMemoryDatabase("FleetDatabase"));

            // Singleton for the mock repository to persist data in memory during runtime
            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddScoped<RentingManager>();
            return services;
        }
    }
}
