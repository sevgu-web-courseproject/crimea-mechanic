using System;
using AutoMapper;
using BusinessLogic.Objects.Car;
using Common.Enums;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class UserCarProfile : Profile
    {
        public UserCarProfile()
        {
            CreateMap<AddOrEditUserCarDto, UserCar>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Model, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.Applications, opt => opt.Ignore());

            CreateMap<UserCar, UserCarDto>()
                .ForMember(d => d.Mark, opt => opt.MapFrom(s => s.Model.Mark.Name))
                .ForMember(d => d.Model, opt => opt.MapFrom(s => s.Model.Name))
                .ForMember(d => d.FuelTypeDescription, opt => opt.MapFrom(s => s.FuelType.GetDescription()))
                .ForMember(d => d.Year, opt => opt.MapFrom(s => s.Year.ToString()));

            CreateMap<Application, ApplicationForCarHistoryDto>()
                .ForMember(d => d.StateDescription, opt => opt.MapFrom(s => s.State.GetDescription()));

            CreateMap<UserCar, UserCarFullDto>()
                .IncludeBase<UserCar, UserCarDto>()
                .ForMember(d => d.Applications, opt => opt.Ignore());
        }
    }
}
