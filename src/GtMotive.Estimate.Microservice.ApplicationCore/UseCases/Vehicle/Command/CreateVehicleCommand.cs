using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// DTO for create a vehicle.
    /// </summary>
    public record CreateVehicleCommand(string VehicleId, int FabricationYear) : IRequest;
}
