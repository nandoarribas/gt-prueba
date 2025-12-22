using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Command;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.Queries;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases
{
    /// <summary>
    /// API Controller for renting management.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FleetController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves a list of all available vehicles for rent.
        /// </summary>
        /// <returns>A list of <see cref="Vehicle"/> representing the available vehicles.</returns>
        /// <response code="200">Returns the list of available vehicles.</response>
        /// <response code="500">If an unexpected internal error occurs.</response>
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Vehicle>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAvailable()
        {
            var result = await _mediator.Send(new GetAvailableVehiclesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Processes a request to create a vehicle.
        /// </summary>
        /// <param name="command">The cretion request with valid data.</param>
        /// <returns>A <see cref="VehicleDto"/> with the created vehicle and confirmation message.</returns>
        /// <response code="200">Returns a success message when the vehicle is successfully created.</response>
        /// <response code="400">If the business rules are violated (e.g., vehicle exists or vehicle too old).</response>
        /// <response code="500">If an unexpected internal error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Create([FromBody] CreateVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Processes a request to rent a vehicle.
        /// </summary>
        /// <param name="command">The rental request details containing Vehicle ID and Client ID.</param>
        /// <returns>A <see cref="VehicleDto"/> with the updated rental state and confirmation message.</returns>
        /// <response code="200">Returns the updated vehicle information and success message.</response>
        /// <response code="400">If the business rules are violated (e.g., vehicle unavailable or client already has a rental).</response>
        /// <response code="500">If an unexpected internal error occurs.</response>
        [HttpPost("rent")]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)] // Añadimos el Type aquí
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Rent([FromBody] RentVehicleCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Processes the return of a rented vehicle with the specified identifier.
        /// </summary>
        /// <param name="command">The return request details containing Vehicle ID and Client ID.</param>
        /// <returns>A <see cref="VehicleDto"/> with the updated rental state and confirmation message.</returns>
        /// <response code="200">Returns a success message when the vehicle is successfully returned.</response>
        /// <response code="400">Returned if the vehicle is not found or is not currently rented (Business Rule Violation).</response>
        /// <response code="500">Returned if an unexpected technical error occurs.</response> [HttpPost("return/{id}")]
        [HttpPost("return")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Return([FromBody] ReturnVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
