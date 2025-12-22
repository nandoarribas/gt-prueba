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
    /// Process renting vehicle action.
    /// </summary>
    /// <param name="rentingManager">Service used to manage business logic.</param>
    /// <param name="mapper">Service used to map domain entities to DTOs.</param>
    public class RentVehicleHandler(IRentingManager rentingManager, IMapper mapper) : IRequestHandler<RentVehicleCommand, VehicleDto>
    {
        private readonly IRentingManager _rentingManager = rentingManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the execution of the <see cref="RentVehicleCommand"/> to process a vehicle rental.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the rental process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns><see cref="VehicleDto"/> representing the rented vehicle details.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>  public async Task Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        public async Task<VehicleDto> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var vehicle = await _rentingManager.RentVehicle(request.VehicleId, request.ClientId);
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return vehicleDto with { Message = $"Rental completed for {vehicle.Id}" };
        }
    }
}
