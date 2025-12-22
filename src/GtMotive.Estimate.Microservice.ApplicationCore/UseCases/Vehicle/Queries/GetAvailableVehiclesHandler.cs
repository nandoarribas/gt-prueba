using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Queries
{
    /// <summary>
    /// Handler for GetAvailableVehiclesQuery.
    /// <param name="repository"> Repository to retrieve Vehicle from db </param>
    /// <param name="mapper">Service used to map domain entities to DTOs.</param>
    /// </summary>
    public class GetAvailableVehiclesHandler(IVehicleRepository repository, IMapper mapper)
        : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<VehicleDto>>
    {
        private readonly IVehicleRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the execution of the <see cref="GetAvailableVehiclesQuery"/> to process to retrieve vehicles.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the rental process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns>A collection of vehicles.</returns>
        public async Task<IEnumerable<VehicleDto>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            var allVehicles = await _repository.GetAllAsync();
            var allVehiclesDTO = _mapper.Map<IEnumerable<VehicleDto>>(allVehicles);
            return allVehiclesDTO;
        }
    }
}
