using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Host.DependencyInjection
{
    internal static class RentingServices
    {
        public static IServiceCollection AddRentingServices(this IServiceCollection services)
        {
            // Singleton for the mock repository to persist data in memory during runtime
            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddScoped<RentingManager>();
            return services;
        }
    }
}
