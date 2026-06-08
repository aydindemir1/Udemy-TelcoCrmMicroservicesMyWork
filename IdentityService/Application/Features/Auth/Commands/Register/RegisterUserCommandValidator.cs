using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("E-posta boş olamaz").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Ad alanı boş olamaz");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Soyad alanı boş olamaz");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Parola boş olamaz")
                .MinimumLength(8).WithMessage("Parola en az 8 karakter olmalıdır")
                .Matches(@"[A-Z]+").WithMessage("Parola en az bir büyük harf içermelidir")
                .Matches(@"[a-z]+").WithMessage("Parola en az bir küçük harf içermelidir")
                .Matches(@"[0-9]+").WithMessage("Parola en az bir rakam içermelidir")
                .Matches(@"[\!\?\*\.]+").WithMessage("Parola en az bir özel karakter içermelidir");
        }
    }
}
