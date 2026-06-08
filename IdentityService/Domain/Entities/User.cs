using Core.Security.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : Core.Security.Domain.Entities.User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; }
    }
}
