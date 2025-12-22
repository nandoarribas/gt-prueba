using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// DTO for return a vehicle.
    /// </summary>
    public record ReturnVehicleCommand(string VehicleId) : IRequest<VehicleDto>;
}
