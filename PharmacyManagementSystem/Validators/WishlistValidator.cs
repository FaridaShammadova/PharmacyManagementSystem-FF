using FluentValidation;
using PharmacyManagementSystem.ViewModels.Wishlist;

namespace PharmacyManagementSystem.Validators
{
    public class WishlistValidator : AbstractValidator<WishlistCreateViewModel>
    {
        public WishlistValidator()
        {
            RuleFor(x => x.MedicineId)
                .GreaterThan(0);
        }
    }
}
