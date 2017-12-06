using System;
using AutoMapper;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Storage;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();

            CreateMap<AddCityDto, City>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Applications, opt => opt.Ignore())
                .ForMember(d => d.CarServices, opt => opt.Ignore());
        }
    }
}
