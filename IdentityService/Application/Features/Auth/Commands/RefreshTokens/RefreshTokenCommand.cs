using Application.Features.Auth.Responses;
using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokenCommand : ICreateCommand<RefreshedTokenResponse>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
    }
}
