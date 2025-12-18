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
    /// <param name="logger">The logger instance to log actions.</param>
    public class RentingManager(IVehicleRepository repository, ITelemetry telemetry, IAppLogger<RentingManager> logger)
    {
        private readonly IVehicleRepository _repository = repository;
        private readonly ITelemetry _telemetry = telemetry;
        private readonly IAppLogger<RentingManager> _logger = logger;

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

            _logger.LogInformation("Attempting to add vehicle {VehicleId} to the fleet.", vehicle.Id);

            var currentYear = DateTime.UtcNow.Year;
            if ((currentYear - vehicle.FabricationYear) > 5)
            {
                _logger.LogWarning("Vehicle {VehicleId} rejected: Too old ({Year}).", vehicle.Id, vehicle.FabricationYear);
                _telemetry.TrackEvent("AddVehicleToFleetFailed", new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Reason", "VehicleTooOld" },
                    { "FabricationYear", vehicle.FabricationYear.ToString(System.Globalization.CultureInfo.InvariantCulture) },
                    { "CurrentYear", currentYear.ToString(System.Globalization.CultureInfo.InvariantCulture) }
                });
                return false;
            }

            await _repository.CreateAsync(vehicle);
            _logger.LogInformation("Vehicle {VehicleId} successfully added.", vehicle.Id);
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
            _logger.LogInformation("Processing rental request for Vehicle: {VehicleId} by Person: {clientId}.", vehicleId, clientId); 

            if (await _repository.HasActiveRentAsync(clientId))
            {
                _logger.LogWarning("Rental denied: Person {clientId} already rented a vehicle.", clientId);
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

            _logger.LogInformation("Rental completed for {VehicleId}.", vehicleId);
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
                _logger.LogInformation("Released vehicle {VehicleId}.", vehicleId);
            }
        }
    }
}
