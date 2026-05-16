using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Sale;

namespace PharmacyManagementSystem.Mappings
{
    public class SaleMappingProfile : Profile
    {
        public SaleMappingProfile()
        {
            CreateMap<Sale, SaleViewModel>().ReverseMap();
            CreateMap<SaleCreateViewModel, Sale>();

            CreateMap<SaleItem, SaleItemViewModel>()
                .ForMember(dest => dest.MedicineName,
                    opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Medicine.Price));

            CreateMap<SaleItemViewModel, SaleItem>();
            CreateMap<SaleItemCreateViewModel, SaleItem>();
        }
    }
}
