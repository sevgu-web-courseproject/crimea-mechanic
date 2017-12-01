using System;
using System.Linq;
using AutoMapper;
using BusinessLogic.Objects.Application;
using Common.Enums;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateApplicationDto, Application>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Car, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore())
                .ForMember(d => d.Offers, opt => opt.Ignore())
                .ForMember(d => d.Service, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.State, opt => opt.UseValue(ApplicationState.InSearch));

            CreateMap<Application, ApplicationBaseInfoDto>();

            CreateMap<Application, ApplicationShortInfoForUserDto>()
                .IncludeBase<Application, ApplicationBaseInfoDto>()
                .ForMember(d => d.ServiceName, opt => opt.MapFrom(s => s.Service != null ? s.Service.Name : string.Empty))
                .ForMember(d => d.StateDescription, opt => opt.MapFrom(s => s.State.GetDescription()))
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.Name));

            CreateMap<Application, ApplicationInfoForUserDto>()
                .IncludeBase<Application, ApplicationShortInfoForUserDto>()
                .ForMember(d => d.CityId, opt => opt.MapFrom(s => s.City.Id))
                .ForMember(d => d.Offers, opt => opt.Ignore());

            CreateMap<Application, ApplicationShortInfoForServiceDto>()
                .IncludeBase<Application, ApplicationBaseInfoDto>()
                .ForMember(d => d.ContactName, opt => opt.MapFrom(s => s.Car.User.Name))
                .ForMember(d => d.OfferId, opt => opt.Ignore());

            CreateMap<Application, ApplicationInfoForServiceDto>()
                .IncludeBase<Application, ApplicationBaseInfoDto>()
                .ForMember(d => d.ContactName, opt => opt.MapFrom(s => s.Car.User.Name))
                .ForMember(d => d.ContactPhone, opt => opt.MapFrom(s => s.Car.User.Phone))
                .ForMember(d => d.StateDescription, opt => opt.MapFrom(s => s.State.GetDescription()));

            CreateMap<Application, ApplicationShortInfoForAdministratorDto>()
                .IncludeBase<Application, ApplicationBaseInfoDto>()
                .ForMember(d => d.CarServiceId, opt => opt.MapFrom(s => s.Service != null ? s.Service.Id : 0))
                .ForMember(d => d.CarServiceName, opt => opt.MapFrom(s => s.Service != null ? s.Service.Name : string.Empty))
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.StateDescription, opt => opt.MapFrom(s => s.State.GetDescription()))
                .ForMember(d => d.UserContactName, opt => opt.MapFrom(s => s.Car.User.Name))
                .ForMember(d => d.UserProfileId, opt => opt.MapFrom(s => s.Car.User.Id));

            CreateMap<Application, ApplicationInfoForAdministratorDto>()
                .IncludeBase<Application, ApplicationShortInfoForAdministratorDto>();

            CreateMap<AddOfferDto, ApplicationOffer>()
                .ForMember(d => d.Application, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Service, opt => opt.Ignore());

            CreateMap<ApplicationOffer, OfferInfoDto>()
                .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.Service.Id))
                .ForMember(d => d.ServiceName, opt => opt.MapFrom(s => s.Service.Name))
                .ForMember(d => d.Сontent, opt => opt.MapFrom(s => s.Content));
        }
    }
}
