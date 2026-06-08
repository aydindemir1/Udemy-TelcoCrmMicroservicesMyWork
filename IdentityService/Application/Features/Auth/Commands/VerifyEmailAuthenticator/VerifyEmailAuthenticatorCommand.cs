using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticator
{
    public class VerifyEmailAuthenticatorCommand : ICommand
    {
        public string ActivationKey { get; set; }
    }
}
