using Application.Repositories;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Commands.Delete
{
    public class ClearBasketCommandHandler : ICommandHandler<ClearBasketCommand, Unit>
    {
        private readonly IBasketRepository _basketRepository;

        public ClearBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Unit> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {

            var tempBasket = new Basket { BillingAccountId = request.BillingAccountId };
            string basketKey = tempBasket.GetRedisKey();

            await _basketRepository.DeleteAsync(basketKey);
            return Unit.Value;
        }
    }
}
