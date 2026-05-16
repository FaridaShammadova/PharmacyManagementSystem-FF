using FluentValidation;
using PharmacyManagementSystem.ViewModels.PrescriptionRequest;

namespace PharmacyManagementSystem.Validators
{
    public class PrescriptionRequestValidator : AbstractValidator<PrescriptionRequestCreateViewModel>
    {
        public PrescriptionRequestValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.OrderId)
                .GreaterThan(0);
        }
    }
}
