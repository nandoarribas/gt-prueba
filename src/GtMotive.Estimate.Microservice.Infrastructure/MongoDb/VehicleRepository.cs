using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Infrastructure adapter for MongoDB persistence (Mock implementation).
    /// </summary>
    public class VehicleRepository() : IVehicleRepository
    {
        /// <summary>
        /// Important. Use a static list in memory to simulate persistence across different instances.
        /// </summary>
        private static readonly List<Vehicle> _data = [];

        /// <summary>
        /// Creates a new vehicle in the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle data.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task CreateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            _data.Add(vehicle);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await Task.FromResult(_data);
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return await Task.FromResult(_data.FirstOrDefault(v => v.Id == id));
        }

        public async Task<bool> HasActiveRentAsync(string clientId)
        {
            return await Task.FromResult(_data.Any(v => v.CurrentClientID == clientId));
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            await Task.CompletedTask;
        }
    }
}
