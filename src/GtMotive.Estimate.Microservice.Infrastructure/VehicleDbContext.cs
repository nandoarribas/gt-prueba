using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Data
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleDbContext"/> class.
    /// VehicleDbContext constructor.
    /// </summary>
    /// <param name="options">Context options.</param>
    public class VehicleDbContext(DbContextOptions<VehicleDbContext> options) : DbContext(options)
    {
        // Vehicles memory set
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Define the model and relationships using Fluent API.
        /// Include ValueGenerateNever to include plateNumber as non-generated key.
        /// </summary>
        /// <param name="modelBuilder">EF Internal API.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
