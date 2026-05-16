using FluentValidation;
using PharmacyManagementSystem.ViewModels.Cart;

namespace PharmacyManagementSystem.Validators
{
    public class CartItemValidator : AbstractValidator<CartItemCreateViewModel>
    {
        public CartItemValidator()
        {
            RuleFor(x => x.MedicineId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .LessThanOrEqualTo(50);
        }
    }
}
