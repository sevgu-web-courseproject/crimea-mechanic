using System;
using AutoMapper;
using BusinessLogic.Objects.Review;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.MapProfiles
{
    public class CarServiceReviewProfile : Profile
    {
        public CarServiceReviewProfile()
        {
            CreateMap<AddReviewDto, CarServiceReview>()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.Service, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.IsDeleted, opt => opt.UseValue(false));

            CreateMap<CarServiceReview, ReviewInfoDto>()
                .ForMember(d => d.Author, opt => opt.MapFrom(s => s.User.Name));
        }
    }
}
