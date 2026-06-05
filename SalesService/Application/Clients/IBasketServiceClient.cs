using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Clients
{
    public interface IBasketServiceClient
    {
        Task<GetInternalBasketResponse> GetByBillingAccountId(string billingAccountId);
    }
}
