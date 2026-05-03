using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Commands.Create
{
    public class CreateContactMediumCommandValidator : AbstractValidator<CreateContactMediumCommand>
    {
        public CreateContactMediumCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty();
            RuleFor(c => c.Type).IsInEnum();
            RuleFor(c => c.Value).NotEmpty().MaximumLength(200);
            RuleFor(c => c.Value).EmailAddress().When(c => c.Type == ContactMediumType.Email).WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(c => c.Value)
            .Matches(@"^\+?[0-9]{10,15}$") // +905321234567 veya 05321234567 gibi
            .When(c => c.Type == ContactMediumType.MobilePhone || c.Type == ContactMediumType.HomePhone)
            .WithMessage("Geçerli bir telefon numarası giriniz.");
        }
    }
}
