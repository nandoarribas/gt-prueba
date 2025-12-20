using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command
{
    /// <summary>
    /// Process return vehicle action.
    /// </summary>
    /// <param name="rentingManager">Service used to manage business logic.</param>
    public class ReturnVehicleHandler(IRentingManager rentingManager) : IRequestHandler<ReturnVehicleCommand>
    {
        private readonly IRentingManager _rentingManager = rentingManager;

        /// <summary>
        /// Handles the execution of the <see cref="ReturnVehicleCommand"/> to process a vehicle return.
        /// </summary>
        /// <param name="request">The command object containing all necessary data to initiate the return process.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>  public async Task Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        public async Task Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await _rentingManager.ReturnVehicle(request.VehicleId);
        }
    }
}
