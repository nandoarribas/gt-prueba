using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO
{
    /// <summary>
    /// DTO returned for create a vehicle.
    /// </summary>
    public record VehicleDto(string VehicleId, string CurrentClientId, bool IsRented, string Message) : IRequest;
}
