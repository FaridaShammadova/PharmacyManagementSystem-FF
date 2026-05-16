using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.PrescriptionRequest;

namespace PharmacyManagementSystem.Mappings
{
    public class PrescriptionMappingProfile : Profile
    {
        public PrescriptionMappingProfile()
        {
            CreateMap<PrescriptionRequest, PrescriptionRequestViewModel>()
                .ForMember(dest => dest.ReviewedByName,
                    opt => opt.MapFrom(src => src.ReviewedBy.UserName));

            CreateMap<PrescriptionRequestViewModel, PrescriptionRequest>();
            CreateMap<PrescriptionRequestCreateViewModel, PrescriptionRequest>();
        }
    }
}
