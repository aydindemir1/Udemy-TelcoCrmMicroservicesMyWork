using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, Guid, IdentityDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
