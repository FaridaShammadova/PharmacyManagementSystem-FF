using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Cart;

namespace PharmacyManagementSystem.Mappings
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<Cart, CartViewModel>().ReverseMap();

            CreateMap<CartItem, CartItemViewModel>()
                .ForMember(dest => dest.MedicineName,
                    opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Medicine.Price));

            CreateMap<CartItemViewModel, CartItem>();
            CreateMap<CartItemCreateViewModel, CartItem>();
        }
    }
}
