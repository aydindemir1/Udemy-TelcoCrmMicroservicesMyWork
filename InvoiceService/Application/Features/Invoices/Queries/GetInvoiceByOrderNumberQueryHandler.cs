using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Invoices.Queries
{
    public class GetInvoiceByOrderNumberQueryHandler : IQueryHandler<GetInvoiceByOrderNumberQuery, GetInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByOrderNumberQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetInvoiceResponse> Handle(GetInvoiceByOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetProjectedAsync(predicate: x => x.OrderNumber == request.OrderNumber, selector: x => new GetInvoiceResponse
            (
                 x.Id,
                 x.Number,
                 x.BillingAccountName,
                 x.BillingAccountDescription,
                 x.BillingAccountNumber,
                 x.BillingAccountType,
                 x.BillingAddress,
                 x.CustomerName,
                 x.OrderNumber,
                 x.TotalPrice,
                 x.InvoiceItems.Select(i => new InvoiceItemDto
                 {
                     ProductOfferingName = i.ProductOfferingName,
                     PriceName = i.PriceName,
                     PriceType = i.PriceType,
                     Quantity = i.Quantity,
                     UnitPrice = i.UnitPrice
                 }).ToList()
            ));
            return invoice;
        }
    }
}
