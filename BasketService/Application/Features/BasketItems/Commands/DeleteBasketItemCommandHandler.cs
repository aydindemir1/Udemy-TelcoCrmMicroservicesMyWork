using Application.Repositories;
using Core.Abstractions.Cqrs.Command;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BasketItems.Commands
{
    public class DeleteBasketItemCommandHandler : ICommandHandler<DeleteBasketItemCommand, Unit>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketItemCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Unit> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var tempBasket = new Basket { BillingAccountId = request.BillingAccountId };
            string basketKey = tempBasket.GetRedisKey();

            var basket = await _basketRepository.GetAsync(basketKey) ?? throw new BusinessException("Sepet bulunamadı");

            basket.RemoveItemById(request.BasketItemId);

            await _basketRepository.SetAsync(basket);
            return Unit.Value;
        }
    }
}
