using System.Linq;
using AutoMapper;
using BusinessLogic.Objects.CarService;
using Common.Enums;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class CarServiceProfile : Profile
    {
        public CarServiceProfile()
        {
            CreateMap<CarService, CarServiceShortInfoDto>()
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.LogoPhotoId, opt => opt.MapFrom(s => s.Files.FirstOrDefault(file => file.Type == FileType.Logo).Id))
                .ForMember(d => d.AverageMark, opt => opt.MapFrom(s => s.Points / s.Reviews.Count));

            CreateMap<CarService, CarServiceInfoDto>()
                .IncludeBase<CarService, CarServiceShortInfoDto>()
                .ForMember(d => d.PhotosId, opt => opt.Ignore())
                .ForMember(d => d.Phones, opt => opt.Ignore());
        }
    }
}
