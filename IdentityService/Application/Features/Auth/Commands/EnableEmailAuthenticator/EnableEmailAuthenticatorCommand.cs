using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator
{
    public class EnableEmailAuthenticatorCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string VerifyEmailUrl { get; set; }
    }
}
