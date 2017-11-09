using AutoMapper;
using BusinessLogic.Objects;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class WorkTagProfile : Profile
    {
        public WorkTagProfile()
        {
            CreateMap<WorkTag, WorkTagDto>();
        }
    }
}
