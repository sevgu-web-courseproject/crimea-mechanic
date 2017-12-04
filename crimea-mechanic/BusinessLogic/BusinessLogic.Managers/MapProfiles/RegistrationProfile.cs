using System;
using AutoMapper;
using BusinessLogic.Objects;
using BusinessLogic.Objects.User;
using Common.Enums;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegistrationDto, ApplicationUser>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Login))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Login))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<RegistrationUserDto, ApplicationUser>()
                .IncludeBase<RegistrationDto, ApplicationUser>();

            CreateMap<RegistrationCarServiceDto, ApplicationUser>()
                .IncludeBase<RegistrationDto, ApplicationUser>();

            CreateMap<RegistrationUserDto, UserProfile>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ContactName))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.State, opt => opt.UseValue(UserState.Active))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<RegistrationCarServiceDto, CarService>()
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.State, opt => opt.UseValue(CarServiceState.UnderСonsideration))
                .ForMember(d => d.Phones, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ApplicationUser, opt => opt.Ignore())
                .ForMember(d => d.CarTags, opt => opt.Ignore())
                .ForMember(d => d.Files, opt => opt.Ignore())
                .ForMember(d => d.City, opt => opt.Ignore())
                .ForMember(d => d.Applications, opt => opt.Ignore())
                .ForMember(d => d.Offers, opt => opt.Ignore())
                .ForMember(d => d.Points, opt => opt.UseValue(0))
                .ForMember(d => d.Reviews, opt => opt.Ignore())
                .ForMember(d => d.WorkTypes, opt => opt.Ignore());

            CreateMap<string, CarServicePhone>()
                .ForMember(d => d.Number, opt => opt.MapFrom(s => s))
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<FileDto, CarServiceFile>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Path, opt => opt.MapFrom(s => s.Path))
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Type, opt => opt.UseValue(FileType.Photo))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.ApplicationUser.Email));
        }
    }
}
