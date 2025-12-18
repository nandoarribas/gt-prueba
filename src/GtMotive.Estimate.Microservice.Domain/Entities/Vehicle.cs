namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle entity within the renting fleet.
    /// </summary>
    public class Vehicle
    {
        /// <summary>Gets or sets the unique identifier for the vehicle.</summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>Gets or sets the fabrication year of the vehicle.</summary>
        public int FabricationYear { get; set; }

        /// <summary>Gets or sets the ID of the person currently renting the vehicle. Null if available.</summary>
        public string? CurrentClientID { get; set; }

        /// <summary>Gets a value indicating whether the vehicle is available for rent.</summary>
        public bool IsAvailable => string.IsNullOrEmpty(CurrentClientID);
    }
}
