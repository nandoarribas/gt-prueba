using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Impl;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases
{
    /// <summary>
    /// API Controller for renting management.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FleetController"/> class.
    /// </remarks>
    /// <param name="manager">Renting manager instance.</param>
    /// <param name="repo">VehicleRepository instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class FleetController(RentingManager manager, IVehicleRepository repo) : ControllerBase
    {
        /// <summary>
        /// Retrieves all available items.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a collection of items that are currently available. The collection
        /// is empty if no items are available.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAvailable() => Ok((await repo.GetAllAsync()).Where(v => v.IsAvailable));

        /// <summary>
        /// Adds a new vehicle to the fleet.
        /// </summary>
        /// <remarks>This action is typically invoked via an HTTP POST request. The request body should
        /// contain the vehicle details to be added.</remarks>
        /// <param name="v">The <see cref="Vehicle"/> to add to the fleet. The vehicle's age must not exceed 5 years.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns <see cref="OkResult"/> if the
        /// vehicle is added successfully; otherwise, returns <see cref="BadRequestObjectResult"/> with an error message
        /// if the vehicle's age exceeds 5 years.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Vehicle v) => await manager.AddVehicleToFleet(v) ? Ok() : BadRequest("Vehicle age exceeds 5 years.");

        /// <summary>
        /// Initiates the rental process for a specified vehicle by a specified person.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be rented. Cannot be null or empty.</param>
        /// <param name="personId">The unique identifier of the person renting the vehicle. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the rental operation. Returns <see
        /// cref="OkObjectResult"/> if the rental is successful; otherwise, returns <see cref="BadRequestObjectResult"/>
        /// with details of the failure.</returns>
        [HttpPost("rent")]
        public async Task<IActionResult> Rent(string vehicleId, string personId)
        {
            var result = await manager.RentVehicle(vehicleId, personId);
            return result.Contains("successful", System.StringComparison.OrdinalIgnoreCase) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Processes the return of a rented vehicle with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to be returned. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("return/{id}")]
        public async Task<IActionResult> Return(string id)
        {
            await manager.ReturnVehicle(id);
            return Ok();
        }
    }
}
