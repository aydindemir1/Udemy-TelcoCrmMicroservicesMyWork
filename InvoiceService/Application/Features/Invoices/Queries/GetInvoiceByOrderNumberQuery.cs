using Core.Abstractions.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Invoices.Queries
{
    public class GetInvoiceByOrderNumberQuery : IQuery<GetInvoiceResponse> //, IAuthenticationRequest
    {
        public string OrderNumber { get; set; }
    }
}
