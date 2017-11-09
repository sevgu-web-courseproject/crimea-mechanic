using AutoMapper;
using BusinessLogic.Objects;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class CarMarkProfile : Profile
    {
        public CarMarkProfile()
        {
            CreateMap<CarMark, CarMarkDto>();
        }
    }
}
