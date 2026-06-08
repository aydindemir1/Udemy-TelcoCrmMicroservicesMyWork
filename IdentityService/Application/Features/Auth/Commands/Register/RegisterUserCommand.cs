using Application.Features.Auth.Responses;
using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommand : ICreateCommand<RegisteredResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
