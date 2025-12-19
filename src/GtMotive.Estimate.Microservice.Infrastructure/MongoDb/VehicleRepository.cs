using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Infrastructure adapter for in memory persistence (Mock implementation).
    /// </summary>
    public class VehicleRepository(VehicleDbContext context) : IVehicleRepository
    {
        /// <summary>
        /// Important. Use a list in memory to simulate persistence across different instances.
        /// </summary>
        private readonly VehicleDbContext _context = context;

        /// <summary>
        /// Creates a new vehicle in the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle data.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task CreateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> HasActiveRentAsync(string clientId)
        {
            return await _context.Vehicles.AnyAsync(v => v.CurrentClientID == clientId);
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
