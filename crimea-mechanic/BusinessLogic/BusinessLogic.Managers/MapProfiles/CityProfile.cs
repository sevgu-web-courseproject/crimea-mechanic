using AutoMapper;
using BusinessLogic.Objects;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
        }
    }
}
