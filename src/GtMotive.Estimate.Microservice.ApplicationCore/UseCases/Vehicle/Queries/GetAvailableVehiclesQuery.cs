using System.Collections.Generic;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Queries
{
    /// <summary>
    /// Query to retrieve all available vehicles.
    /// </summary>
    public record GetAvailableVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;
}
