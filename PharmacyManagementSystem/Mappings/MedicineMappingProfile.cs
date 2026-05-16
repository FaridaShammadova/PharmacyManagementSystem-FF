using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Medicine;

namespace PharmacyManagementSystem.Mappings
{
    public class MedicineMappingProfile : Profile
    {
        public MedicineMappingProfile()
        {
            CreateMap<Medicine, MedicineViewModel>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<MedicineViewModel, Medicine>();
            CreateMap<Medicine, MedicineViewModel>();

            CreateMap<MedicineCreateViewModel, Medicine>();

            CreateMap<MedicineEditViewModel, Medicine>();
        }
    }
}
