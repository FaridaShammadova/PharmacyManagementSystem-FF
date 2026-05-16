using FluentValidation;
using PharmacyManagementSystem.ViewModels.Medicine;

namespace PharmacyManagementSystem.Validators
{
    public class MedicineValidator : AbstractValidator<MedicineCreateViewModel>
    {
        public MedicineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.ExpireDate)
                .GreaterThan(DateTime.Today)
                .WithMessage("Expire date must be in future");

            RuleFor(x => x.Barcode)
                .NotEmpty()
                .Length(5, 50);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}
