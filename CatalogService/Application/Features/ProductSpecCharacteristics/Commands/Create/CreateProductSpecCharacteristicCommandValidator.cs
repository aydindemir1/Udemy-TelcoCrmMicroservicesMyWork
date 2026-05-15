using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristics.Commands.Create
{
    public class CreateProductSpecCharacteristicCommandValidator : AbstractValidator<CreateProductSpecCharacteristicCommand>
    {
        public CreateProductSpecCharacteristicCommandValidator()
        {
            RuleFor(x => x.ProductSpecificationId)
                .NotEmpty().WithMessage("ProductSpecificationId boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Özellik adı boş olamaz.")
                .MaximumLength(150).WithMessage("Özellik adı 150 karakterden uzun olamaz.");

            RuleFor(x => x.ValueType)
                .IsInEnum().WithMessage("Geçersiz değer tipi.");
        }
    }
}
