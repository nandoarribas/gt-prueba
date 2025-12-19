using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using MediatR;
using VehicleR = GtMotive.Estimate.Microservice.Domain.Entities.Vehicle;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// Process create vehicle action.
    /// </summary>
    /// <param name="rentingManager">Service used to manage business logic.</param>
    public class CreateVehicleHandler(RentingManager rentingManager) : IRequestHandler<CreateVehicleCommand>
    {
        private readonly RentingManager _rentingManager = rentingManager;

        /// <summary>
        /// Handles the execution of the <see cref="CreateVehicleCommand"/> to process a vehicle creation.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the creation process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>  public async Task Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var vehicle = new VehicleR
            {
                Id = request.VehicleId,
                FabricationYear = request.FabricationYear,
                CurrentClientID = null
            };

            await _rentingManager.AddVehicleToFleet(vehicle);
        }
    }
}
