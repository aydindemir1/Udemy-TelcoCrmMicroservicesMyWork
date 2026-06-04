using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Clients
{
    public interface ICustomerServiceClient
    {
        Task<GetInternalBillingAccountResponse> GetByBillingAccountId(Guid billingAccountId);
    }
}
