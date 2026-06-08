using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, Guid, IdentityDbContext>, IEmailAuthenticatorRepository
    {
        public EmailAuthenticatorRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
