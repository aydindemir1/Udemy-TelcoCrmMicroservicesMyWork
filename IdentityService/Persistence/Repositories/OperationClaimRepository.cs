using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class OperationClaimRepository : EfRepositoryBase<OperationClaim, Guid, IdentityDbContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
