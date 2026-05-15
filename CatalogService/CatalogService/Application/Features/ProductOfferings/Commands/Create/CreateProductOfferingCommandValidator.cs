using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Commands.Create
{
    public class CreateProductOfferingCommandValidator : AbstractValidator<CreateProductOfferingCommand>
    {
        public CreateProductOfferingCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId boş olamaz.");

            RuleFor(x => x.ProductSpecificationId)
                .NotEmpty().WithMessage("ProductSpecificationId boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MaximumLength(150).WithMessage("İsim 150 karakterden uzun olamaz.");

            RuleFor(x => x.Description)
                .MaximumLength(150).WithMessage("Açıklama 500 karakterden uzun olamaz.");

            RuleFor(x => x.ValidFrom)
                .LessThan(x => x.ValidTo ?? DateTime.MaxValue)
                .WithMessage("Başlangıç tarihi bitiş tarihinden sonra olamaz.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Geçersiz durum değeri.");
        }
    }
}
