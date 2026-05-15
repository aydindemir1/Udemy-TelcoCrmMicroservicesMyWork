using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferingPrices.Commands.Create
{
    public class CreateProductOfferingPriceCommandValidator : AbstractValidator<CreateProductOfferingPriceCommand>
    {
        public CreateProductOfferingPriceCommandValidator()
        {
            RuleFor(x => x.ProductOfferingId)
                .NotEmpty().WithMessage("ProductOfferingId boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Fiyat adı boş olamaz.")
                .MaximumLength(150).WithMessage("Fiyat adı 150 karakterden uzun olamaz.");

            RuleFor(x => x.PriceType)
                .IsInEnum().WithMessage("Geçersiz fiyat tipi.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Tutar sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Para birimi boş olamaz.")
                .Length(3).WithMessage("Para birimi 3 karakter olmalıdır.");
        }
    }
}
