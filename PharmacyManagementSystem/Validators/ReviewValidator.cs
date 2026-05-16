using FluentValidation;
using PharmacyManagementSystem.ViewModels.Review;

namespace PharmacyManagementSystem.Validators
{
    public class ReviewValidator : AbstractValidator<ReviewCreateViewModel>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.MedicineId)
                .GreaterThan(0);

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5");

            RuleFor(x => x.Comment)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(300);
        }
    }
}
