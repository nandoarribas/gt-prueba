using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// DTO for renting a vehicle.
    /// </summary>
    public record RentVehicleCommand(string VehicleId, string ClientId) : IRequest<Unit>;
}
