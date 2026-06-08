using Core.Security.Domain.Enums;
using Core.Security.Jwt;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Responses
{
    public class LoggedResponse
    {
        public Guid? UserId { get; set; }
        public string Email { get; set; }
        public AccessToken? AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public AuthenticatorType AuthenticatorType { get; set; }

        public LoggedHttpResponse ToResponse()
         => new LoggedHttpResponse
         {
             UserId = UserId,
             Email = Email,
             AccessToken = AccessToken,
             AuthenticatorType = AuthenticatorType
         };


        public class LoggedHttpResponse
        {
            public Guid? UserId { get; set; }
            public string Email { get; set; }
            public AccessToken AccessToken { get; set; }
            public AuthenticatorType AuthenticatorType { get; set; }
        }

    }
}
