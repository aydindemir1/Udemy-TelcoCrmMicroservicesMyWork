using Core.Abstractions.Repositories;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IInvoiceRepository : IAsyncRepository<Invoice, Guid>
    {
    }
}
