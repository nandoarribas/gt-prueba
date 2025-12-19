using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using VehicleR = GtMotive.Estimate.Microservice.Domain.Entities.Vehicle;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Queries
{
    /// <summary>
    /// Handler for GetAvailableVehiclesQuery.
    /// </summary>
    public class GetAvailableVehiclesHandler(IVehicleRepository repository)
        : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<VehicleR>>
    {
        private readonly IVehicleRepository _repository = repository;

        /// <summary>
        /// Handles the execution of the <see cref="GetAvailableVehiclesQuery"/> to process to retrieve vehicles.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the rental process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns>A collection of vehicles.</returns>
        public async Task<IEnumerable<VehicleR>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            var allVehicles = await _repository.GetAllAsync();
            return allVehicles;
        }
    }
}
