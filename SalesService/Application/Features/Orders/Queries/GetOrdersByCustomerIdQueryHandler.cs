using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Queries
{
    public class GetOrdersByCustomerIdQueryHandler : IQueryHandler<GetOrdersByCustomerIdQuery, List<GetOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByCustomerIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderResponse>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(predicate: x => x.CustomerId == request.CustomerId);

            var result = orders.Select(o => new GetOrderResponse
            {
                Id = o.Id.ToString(),
                BillingAccountId = o.BillingAccountId,
                BillingAccountNumber = o.BillingAccountNumber,
                BillingAccountName = o.BillingAccountName,
                BillingAccountDescription = o.BillingAccountDescription,
                BillingAccountType = o.BillingAccountType,
                BillingAddress = o.BillingAddress,
                CustomerId = o.CustomerId,
                CustomerName = o.CustomerName,
                TotalPrice = o.TotalPrice,
                Items = o.Items.Select(i => new GetOrderItemResponse
                {
                    ProductOfferId = i.ProductOfferId,
                    ProductOfferName = i.ProductOfferName,
                    PriceName = i.PriceName,
                    PriceType = i.PriceType,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()


            }).ToList();

            return result;
        }
    }
}
