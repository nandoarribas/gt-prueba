using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl
{
    /// <summary>
    /// Handles the business logic for renting operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentingManager"/> class.
    /// </remarks>
    /// <param name="repository">The vehicle repository to use for data access.</param>
    /// <param name="telemetry">The telemetry instance to detect executed actions.</param>
    public class RentingManager(IVehicleRepository repository, ITelemetry telemetry)
    {
        private readonly IVehicleRepository _repository = repository;
        private readonly ITelemetry _telemetry = telemetry;

        /// <summary>
        /// Adds a vehicle to the fleet if it is not older than 5 years.
        /// </summary>
        /// <param name="vehicle">Vehicle to add.</param>
        /// <returns>
        /// <c>true</c> if add was ok. otherwise, <c>false</c> if vehicle older than 5 years.
        /// </returns>
        public async Task<bool> AddVehicleToFleet(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var currentYear = DateTime.UtcNow.Year;
            if ((currentYear - vehicle.FabricationYear) > 5)
            {
                _telemetry.TrackEvent("AddVehicleToFleetFailed", new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Reason", "VehicleTooOld" },
                    { "FabricationYear", vehicle.FabricationYear.ToString(System.Globalization.CultureInfo.InvariantCulture) },
                    { "CurrentYear", currentYear.ToString(System.Globalization.CultureInfo.InvariantCulture) }
                });
                return false;
            }

            await _repository.CreateAsync(vehicle);
            _telemetry.TrackEvent("AddVehicleToFleetSuccess", new System.Collections.Generic.Dictionary<string, string>
            {
                { "VehicleId", vehicle.Id }
            });
            return true;
        }

        /// <summary>Processes a rental request validating business constraints.</summary>
        /// <param name="vehicleId">Vehicle Identifier.</param>
        /// <param name="clientId">Client identifier.</param>
        /// <returns>
        /// A message indicating result
        /// "This person already has an active rental.",
        /// "Vehicle not found or not available." ,
        /// or "Rental successful." if process was ok.
        /// </returns>
        public async Task<string> RentVehicle(string vehicleId, string clientId)
        {
            if (await _repository.HasActiveRentAsync(clientId))
            {
                _telemetry.TrackEvent("RentVehicleFailed", new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Reason", "ActiveRentalExists" },
                    { "ClientId", clientId }
                });
                return "This person already has an active rental.";
            }

            var vehicle = await _repository.GetByIdAsync(vehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
            {
                _telemetry.TrackEvent("RentVehicleFailed", new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Reason", "VehicleNotFoundOrUnavailable" },
                    { "VehicleId", vehicleId }
                });
                return "Vehicle not found or not available.";
            }

            vehicle.CurrentClientID = clientId;
            await _repository.UpdateAsync(vehicle);
            _telemetry.TrackEvent("RentVehicleSuccess", new System.Collections.Generic.Dictionary<string, string>
            {
                { "VehicleId", vehicleId },
                { "ClientId", clientId }
            });
            return "Rental successful.";
        }

        /// <summary>Processes the return of a vehicle.</summary>
        /// <param name="vehicleId">Vehicle id to return.</param>
        /// <returns>An async task with return vehicle action.</returns>
        public async Task ReturnVehicle(string vehicleId)
        {
            var vehicle = await _repository.GetByIdAsync(vehicleId);
            if (vehicle != null)
            {
                vehicle.CurrentClientID = null;
                await _repository.UpdateAsync(vehicle);
            }
        }
    }
}
