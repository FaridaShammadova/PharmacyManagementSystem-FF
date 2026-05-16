using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Wishlist;

namespace PharmacyManagementSystem.Mappings
{
    public class WishlistMappingProfile : Profile
    {
        public WishlistMappingProfile()
        {
            CreateMap<Wishlist, WishlistViewModel>().ReverseMap();

            CreateMap<WishlistItem, WishlistItemViewModel>()
                .ForMember(dest => dest.MedicineName,
                    opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Medicine.ImageUrl));

            CreateMap<WishlistItemViewModel, WishlistItem>();
            CreateMap<WishlistCreateViewModel, WishlistItem>();
        }
    }
}
