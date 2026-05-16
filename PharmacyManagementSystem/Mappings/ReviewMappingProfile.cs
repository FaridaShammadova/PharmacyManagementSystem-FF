using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Review;

namespace PharmacyManagementSystem.Mappings
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, ReviewViewModel>()
               .ForMember(dest => dest.UserName,
                   opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<ReviewViewModel, Review>();
            CreateMap<ReviewCreateViewModel, Review>();
        }
    }
}
