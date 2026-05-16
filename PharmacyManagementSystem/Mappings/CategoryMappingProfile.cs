using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Category;

namespace PharmacyManagementSystem.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<CategoryCreateViewModel, Category>();
        }
    }
}
