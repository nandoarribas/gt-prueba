using System;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>  public async Task Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        public async Task<IEnumerable<VehicleR>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            var allVehicles = await _repository.GetAllAsync();
            return allVehicles;
        }
    }
}
