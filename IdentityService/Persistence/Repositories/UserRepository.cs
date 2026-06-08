using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, Guid, IdentityDbContext>, IUserRepository
    {
        public UserRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
