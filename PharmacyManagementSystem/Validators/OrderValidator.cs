using FluentValidation;
using PharmacyManagementSystem.Enums;
using PharmacyManagementSystem.ViewModels.Order;

namespace PharmacyManagementSystem.Validators
{
    public class OrderValidator : AbstractValidator<OrderCreateViewModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.DeliveryAddress)
                .NotEmpty()
                .WithMessage("Çatdırılma ünvanı mütləqdir.")
                .MinimumLength(10)
                .WithMessage("Ünvan minimum 10 simvol olmalıdır.")
                .MaximumLength(200)
                .WithMessage("Ünvan maksimum 200 simvol ola bilər.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Telefon nömrəsi mütləqdir.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email mütləqdir.")
                .EmailAddress()
                .WithMessage("Düzgün email daxil edin.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .NotNull()
                .WithMessage("Ödəniş üsulu seçin.");

            RuleFor(x => x.CardHolderName)
                .NotEmpty()
                .When(x => x.PaymentMethod == PaymentMethod.Card)
                .WithMessage("Kart sahibinin adı mütləqdir.");

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .When(x => x.PaymentMethod == PaymentMethod.Card)
                .WithMessage("Kart nömrəsi mütləqdir.");

            RuleFor(x => x.ExpireDate)
                .NotEmpty()
                .When(x => x.PaymentMethod == PaymentMethod.Card)
                .WithMessage("Kartın son istifadə tarixi mütləqdir.");

            RuleFor(x => x.CVV)
                .NotEmpty()
                .When(x => x.PaymentMethod == PaymentMethod.Card)
                .WithMessage("CVV mütləqdir.")
                .Length(3)
                .WithMessage("CVV 3 rəqəm olmalıdır.");
        }
    }
}
