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

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRentingManager, RentingManager>();
            return services;
        }
    }
}
