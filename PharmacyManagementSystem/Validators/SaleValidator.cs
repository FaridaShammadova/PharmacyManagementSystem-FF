using FluentValidation;
using PharmacyManagementSystem.ViewModels.Sale;

namespace PharmacyManagementSystem.Validators
{
    public class SaleValidator : AbstractValidator<SaleCreateViewModel>
    {
        public SaleValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("At least one item is required");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.MedicineId).GreaterThan(0);
                item.RuleFor(x => x.Quantity).GreaterThan(0);
            });
        }
    }
}
