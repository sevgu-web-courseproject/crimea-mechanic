using System;
using AutoMapper;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Storage;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class CarModelProfile : Profile
    {
        public CarModelProfile()
        {
            CreateMap<CarModel, CarModelDto>();

            CreateMap<AddCarModelDto, CarModel>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Mark, opt => opt.Ignore())
                .ForMember(d => d.UserCars, opt => opt.Ignore());
        }
    }
}
