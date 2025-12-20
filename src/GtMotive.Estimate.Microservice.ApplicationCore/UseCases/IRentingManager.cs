using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl
{
    /// <summary>
    /// Handles the business logic for renting operations.
    /// </summary>
    public interface IRentingManager
    {
        /// <summary>
        /// Adds a vehicle to the fleet if it is not older than 5 years.
        /// </summary>
        /// <param name="vehicle">Vehicle to add.</param>
        /// <returns>
        /// A <see cref="Task"/> represents async operation with the result of the rental process.
        /// </returns>
        Task AddVehicleToFleet(Domain.Entities.Vehicle vehicle);

        /// <summary>Processes a rental request validating business constraints.</summary>
        /// <param name="vehicleId">Vehicle Identifier.</param>
        /// <param name="clientId">Client identifier.</param>
        /// <returns>
        /// A <see cref="Task"/> represents async operation with the result of the rental process.
        /// </returns>
        Task RentVehicle(string vehicleId, string clientId);

        /// <summary>Processes the return of a vehicle.</summary>
        /// <param name="vehicleId">Vehicle id to return.</param>
        /// <returns>An async task with return vehicle action.</returns>
        Task ReturnVehicle(string vehicleId);
    }
}
