using Application.Clients;
using Newtonsoft.Json;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Clients
{
    public class CustomerServiceClient : ICustomerServiceClient
    {
        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetInternalBillingAccountResponse> GetByBillingAccountId(Guid billingAccountId)
        {
            var response = await _httpClient.GetAsync($"api/internal/accounts/{billingAccountId}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new HttpRequestException($"Http Error {response.StatusCode} : {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetInternalBillingAccountResponse>(json);
        }


    }
}
