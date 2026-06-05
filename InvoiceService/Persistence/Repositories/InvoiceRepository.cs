using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entites;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class InvoiceRepository : EfRepositoryBase<Invoice, Guid, InvoiceDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}
