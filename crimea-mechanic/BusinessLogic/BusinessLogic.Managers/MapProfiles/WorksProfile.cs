using AutoMapper;
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

            CreateMap<WorkType, WorkTypeDto>();
        }
    }
}
