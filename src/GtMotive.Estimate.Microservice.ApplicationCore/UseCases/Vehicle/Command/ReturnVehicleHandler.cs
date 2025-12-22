using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// Process return vehicle action.
    /// </summary>
    /// <param name="rentingManager">Service used to manage business logic.</param>
    /// <param name="mapper">Service used to map domain entities to DTOs.</param>
    public class ReturnVehicleHandler(IRentingManager rentingManager, IMapper mapper) : IRequestHandler<ReturnVehicleCommand, VehicleDto>
    {
        private readonly IRentingManager _rentingManager = rentingManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the execution of the <see cref="ReturnVehicleCommand"/> to process a vehicle return.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the return process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>  public async Task Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        public async Task<VehicleDto> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var vehicle = await _rentingManager.ReturnVehicle(request.VehicleId);

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return vehicleDto with { Message = $"Released vehicle {vehicle.Id}" };
        }
    }
}
