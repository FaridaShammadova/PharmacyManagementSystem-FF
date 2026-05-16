using FluentValidation;
using PharmacyManagementSystem.ViewModels.Category;

namespace PharmacyManagementSystem.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryCreateViewModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MinimumLength(3).WithMessage("Minimum 3 characters required")
                .MaximumLength(50).WithMessage("Maximum 50 characters allowed");
        }
    }
}
