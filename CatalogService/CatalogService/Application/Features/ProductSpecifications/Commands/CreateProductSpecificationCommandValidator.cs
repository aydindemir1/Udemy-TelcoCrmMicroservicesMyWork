using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Commands
{
    public class CreateProductSpecificationCommandValidator : AbstractValidator<CreateProductSpecificationCommand>
    {
        public CreateProductSpecificationCommandValidator()
        {
            RuleFor(x => x.ModelId)
                .GreaterThan((short)0).WithMessage("Model ID sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MaximumLength(150).WithMessage("İsim 150 karakterden uzun olamaz.");

            RuleFor(x => x.ProductType)
                .IsInEnum().WithMessage("Geçersiz ürün tipi.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");

            RuleFor(x => x.LifecycleStatus)
                .IsInEnum().WithMessage("Geçersiz lifecycle durumu.");
        }
    }
}
