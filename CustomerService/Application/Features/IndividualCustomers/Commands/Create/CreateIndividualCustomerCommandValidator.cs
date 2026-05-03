using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.Create
{
    public class CreateIndividualCustomerCommandValidator : AbstractValidator<CreateIndividualCustomerCommand>
    {
        public CreateIndividualCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("İsim alanı boş olamaz").MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır").MaximumLength(50);
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Soy isim alanı boş olamaz").MinimumLength(2).WithMessage("Soy isim en az 2 karakter olmalıdır").MaximumLength(50);
            RuleFor(c => c.NationalIdentity).NotEmpty().Length(11).WithMessage("TC Kimlik Numarası 11 haneli olmalıdır").Matches("^[1-9]{1}[0-9]{9}[02468]{1}$").WithMessage("Geçersiz TC Kimlik numarası formatı");
            RuleFor(c => c.BirthDate).NotEmpty().LessThan(DateTime.Now.AddYears(-18)).WithMessage("Müşteri en az 18 yaşında olmalıdır");
        }
    }
}
