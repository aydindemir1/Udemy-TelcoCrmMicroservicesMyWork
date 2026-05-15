using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristicValues.Commands.Create
{
    public class CreateProductSpecCharacteristicValueCommandValidator : AbstractValidator<CreateProductSpecCharacteristicValueCommand>
    {
        public CreateProductSpecCharacteristicValueCommandValidator()
        {
            RuleFor(x => x.ProductSpecCharacteristicId)
                .NotEmpty().WithMessage("ProductSpecCharacteristicId boş olamaz.");

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Değer boş olamaz.")
                .MaximumLength(200).WithMessage("Değer 200 karakterden uzun olamaz.");
        }
    }
}
