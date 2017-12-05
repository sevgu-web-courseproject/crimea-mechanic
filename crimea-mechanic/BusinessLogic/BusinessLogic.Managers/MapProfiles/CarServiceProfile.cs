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
                .ForMember(d => d.LogoPhotoId, opt => opt.MapFrom(s => s.Files.SingleOrDefault(file => !file.IsDeleted && file.Type == FileType.Logo).Id))
                .ForMember(d => d.AverageMark, opt => opt.Ignore());

            CreateMap<CarService, CarServiceInfoDto>()
                .IncludeBase<CarService, CarServiceShortInfoDto>()
                .ForMember(d => d.PhotosId, opt => opt.Ignore())
                .ForMember(d => d.Phones, opt => opt.Ignore())
                .ForMember(d => d.ReviewId, opt => opt.Ignore())
                .ForMember(d => d.Reviews, opt => opt.Ignore())
                .ForMember(d => d.WorkClasses, opt => opt.Ignore());

            CreateMap<CarService, RegistrationRequestShortInfoDto>()
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.LogoPhotoId, opt => opt.MapFrom(s => s.Files.SingleOrDefault(file => !file.IsDeleted && file.Type == FileType.Logo).Id));

            CreateMap<CarService, RegistrationRequestInfoDto>()
                .IncludeBase<CarService, RegistrationRequestShortInfoDto>()
                .ForMember(d => d.PhotosId, opt => opt.MapFrom(s => s.Files.Where(file => file.Type == FileType.Photo).Select(file => file.Id)))
                .ForMember(d => d.Phones, opt => opt.MapFrom(s => s.Phones.Where(p => !p.IsDeleted).Select(p => p.Number)))
                .ForMember(d => d.WorkClasses, opt => opt.Ignore());

            CreateMap<CarService, CarServiceInfoForEditDto>()
                .ForMember(d => d.LogoId, opt => opt.MapFrom(s => s.Files.SingleOrDefault(file => !file.IsDeleted && file.Type == FileType.Logo).Id))
                .ForMember(d => d.Phones, opt => opt.MapFrom(s => s.Phones.Where(phone => !phone.IsDeleted).Select(phone => phone.Number)))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Files.Where(file => !file.IsDeleted && file.Type == FileType.Photo).Select(file => file.Id)))
                .ForMember(d => d.WorkTypes, opt => opt.MapFrom(s => s.WorkTypes.Select(w => w.Id)));
        }
    }
}
