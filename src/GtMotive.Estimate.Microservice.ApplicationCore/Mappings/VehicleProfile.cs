using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.DTO;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Mappings
{
    /// <summary>
    /// Class to map Vehicle entities to Vehicle DTOs.
    /// </summary>
    public class VehicleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleProfile"/> class.
        /// </summary>
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ConstructUsing(src => new VehicleDto(
                    src.Id,
                    src.CurrentClientID,
                    !string.IsNullOrEmpty(src.CurrentClientID),
                    string.Empty));
        }
    }
}
