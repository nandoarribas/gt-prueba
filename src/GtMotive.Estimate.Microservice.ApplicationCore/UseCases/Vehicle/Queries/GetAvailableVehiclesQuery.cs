using System.Collections.Generic;
using MediatR;
using VehicleR = GtMotive.Estimate.Microservice.Domain.Entities.Vehicle;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Queries
{
    /// <summary>
    /// Query to retrieve all available vehicles.
    /// </summary>
    public record GetAvailableVehiclesQuery : IRequest<IEnumerable<VehicleR>>;
}
