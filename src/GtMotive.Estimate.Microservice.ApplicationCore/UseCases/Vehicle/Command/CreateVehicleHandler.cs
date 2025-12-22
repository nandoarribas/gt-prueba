using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using MediatR;
using VehicleR = GtMotive.Estimate.Microservice.Domain.Entities.Vehicle;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// Process create vehicle action.
    /// </summary>
    /// <param name="rentingManager">Service used to manage business logic.</param>
    /// <param name="mapper">Service used to map domain entities to DTOs.</param>
    public class CreateVehicleHandler(IRentingManager rentingManager, IMapper mapper) : IRequestHandler<CreateVehicleCommand, VehicleDto>
    {
        private readonly IRentingManager _rentingManager = rentingManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the execution of the <see cref="CreateVehicleCommand"/> to process a vehicle creation.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the creation process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns>A <see cref="VehicleDto"/> representing the created vehicle details.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>  public async Task Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var vehicle = new VehicleR
            {
                Id = request.VehicleId,
                FabricationYear = request.FabricationYear,
                CurrentClientID = null
            };

            vehicle = await _rentingManager.AddVehicleToFleet(vehicle);

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return vehicleDto with { Message = $"Vehicle {vehicle.Id} successfully added." };
        }
    }
}
