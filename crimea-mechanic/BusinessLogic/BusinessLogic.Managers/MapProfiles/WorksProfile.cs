using System;
using AutoMapper;
using BusinessLogic.Objects.Storage;
using BusinessLogic.Objects.Works;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class WorksProfile : Profile
    {
        public WorksProfile()
        {
            CreateMap<WorkClass, WorkClassDto>()
                .ForMember(d => d.Types, opt => opt.Ignore());

            CreateMap<AddWorkClassDto, WorkClass>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.WorkTypes, opt => opt.Ignore());

            CreateMap<WorkType, WorkTypeDto>();

            CreateMap<AddWorkTypeDto, WorkType>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Applications, opt => opt.Ignore())
                .ForMember(d => d.CarServices, opt => opt.Ignore())
                .ForMember(d => d.Class, opt => opt.Ignore());
        }
    }
}
