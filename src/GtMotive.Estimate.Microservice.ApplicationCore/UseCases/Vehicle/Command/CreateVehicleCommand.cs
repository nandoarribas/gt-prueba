using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// Entry data for create a vehicle.
    /// </summary>
    public record CreateVehicleCommand(string VehicleId, int FabricationYear) : IRequest<VehicleDto>;
}
