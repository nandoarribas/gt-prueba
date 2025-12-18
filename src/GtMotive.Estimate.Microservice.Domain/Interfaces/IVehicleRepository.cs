using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Output port for vehicle data persistence.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>Persists a new vehicle.</summary>
        /// <param name="vehicle">Entity <see cref="Vehicle"/> to persist.</param>
        /// <returns>A <see cref="Task"/> represents async operation.</returns>
        Task CreateAsync(Vehicle vehicle);

        /// <summary>Retrieves all vehicles from the fleet.</summary>
        /// <returns>A <see cref="Task"/> represents async operation.</returns>
        Task<IEnumerable<Vehicle>> GetAllAsync();

        /// <summary>Finds a vehicle by its unique identifier.</summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>A <see cref="Task"/> represents async operation.</returns>
        Task<Vehicle?> GetByIdAsync(string id);

        /// <summary>Checks if a person already has an active rental.</summary>
        /// <param name="clientId">The unique identifier of the person to check for an active rental.</param>
        /// <returns>A <see cref="Task"/> represents async operation.</returns>
        Task<bool> HasActiveRentAsync(string clientId);

        /// <summary>Updates the state of an existing vehicle.</summary>
        /// <param name="vehicle">Entity <see cref="Vehicle"/> to persist.</param>
        /// <returns>A <see cref="Task"/> represents async operation.</returns>
        Task UpdateAsync(Vehicle vehicle);
    }
}
