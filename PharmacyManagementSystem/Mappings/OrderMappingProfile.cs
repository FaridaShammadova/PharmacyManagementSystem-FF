using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Order;

namespace PharmacyManagementSystem.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<OrderViewModel, Order>();
            CreateMap<OrderCreateViewModel, Order>();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.MedicineName,
                    opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Medicine.Price));

            CreateMap<OrderItemViewModel, OrderItem>();
        }
    }
}
