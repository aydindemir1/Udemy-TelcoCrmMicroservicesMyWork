using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Commands.Create
{
    public class CreateBillingAccountCommandValidator : AbstractValidator<CreateBillingAccountCommand>
    {
        public CreateBillingAccountCommandValidator()
        {
            RuleFor(p => p.CustomerId).NotEmpty().WithMessage("Müşteri ID'si zorunludur.");
            RuleFor(p => p.BillingAddressId).NotEmpty().WithMessage("Fatura adresi ID'si zorunludur.");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Hesap adı zorunludur.").MaximumLength(150).WithMessage("Hesap adı en fazla 150 karakter olabilir.");
            RuleFor(p => p.Description).MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olabilir.");
            RuleFor(p => p.Type).IsInEnum();
        }
    }
}
