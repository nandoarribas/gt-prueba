using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// Entry data for renting a vehicle.
    /// </summary>
    public record RentVehicleCommand(string VehicleId, string ClientId) : IRequest<VehicleDto>;
}
