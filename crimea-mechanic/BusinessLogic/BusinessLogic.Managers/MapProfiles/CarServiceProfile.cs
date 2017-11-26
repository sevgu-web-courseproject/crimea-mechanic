﻿using System.Linq;
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
                .ForMember(d => d.AverageMark, opt => opt.Ignore());

            CreateMap<CarService, CarServiceInfoDto>()
                .IncludeBase<CarService, CarServiceShortInfoDto>()
                .ForMember(d => d.PhotosId, opt => opt.Ignore())
                .ForMember(d => d.Phones, opt => opt.Ignore());

            CreateMap<CarService, RegistrationRequestShortInfoDto>()
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.LogoPhotoId, opt => opt.MapFrom(s => s.Files.FirstOrDefault(file => file.Type == FileType.Logo).Id));

            CreateMap<CarService, RegistrationRequestInfoDto>()
                .IncludeBase<CarService, RegistrationRequestShortInfoDto>()
                .ForMember(d => d.PhotosId, opt => opt.MapFrom(s => s.Files.Where(file => file.Type == FileType.Photo).Select(file => file.Id)));
        }
    }
}
