using Application.Features.BillingAccounts.Rules;
using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using Domain.Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Queries.Internal.GetById
{
    public class GetInternalBillingAccountByIdQueryHandler : IQueryHandler<GetInternalBillingAccountByIdQuery, GetInternalBillingAccountResponse>
    {
        private readonly IBillingAccountRepository _billingAccountRepository;
        private readonly BillingAccountBusinessRules _billingAccountBusinessRules;

        public GetInternalBillingAccountByIdQueryHandler(IBillingAccountRepository billingAccountRepository, BillingAccountBusinessRules billingAccountBusinessRules)
        {
            _billingAccountRepository = billingAccountRepository;
            _billingAccountBusinessRules = billingAccountBusinessRules;
        }

        public async Task<GetInternalBillingAccountResponse> Handle(GetInternalBillingAccountByIdQuery request, CancellationToken cancellationToken)
        {
            await _billingAccountBusinessRules.EnsureBillingAccountExists(request.Id);
            var response = await _billingAccountRepository.GetProjectedAsync(predicate: x => x.Id == request.Id, selector: x => new GetInternalBillingAccountResponse
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                Description = x.Description,
                Status = x.Status.ToString(),
                Type = x.Type.ToString(),
                CustomerId = x.CustomerId,
                CustomerName = x.Customer is IndividualCustomer ? ((IndividualCustomer)x.Customer).FirstName + " " + ((IndividualCustomer)x.Customer).LastName : "Unknown",
                AddressHouseNumber = x.BillingAddress.HouseNumber,
                AddressStreet = x.BillingAddress.Street,
                CityName = x.BillingAddress.District.City.Name,
                DistrictName = x.BillingAddress.District.Name,

            }, cancellationToken: cancellationToken);
            return response;
        }
    }
}
