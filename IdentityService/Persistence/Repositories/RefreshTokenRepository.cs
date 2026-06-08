using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, Guid, IdentityDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
