using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Clients
{
    public interface ICatalogServiceClient
    {
        Task<GetInternalProductOfferResponse> GetByProductOfferId(Guid productOfferId);
    }
}
