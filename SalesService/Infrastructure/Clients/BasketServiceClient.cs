using Application.Clients;
using Newtonsoft.Json;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Clients
{
    public class BasketServiceClient : IBasketServiceClient
    {
        private readonly HttpClient _httpClient;

        public BasketServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetInternalBasketResponse> GetByBillingAccountId(string billingAccountId)
        {
            var response = await _httpClient.GetAsync($"/api/internal/baskets/{billingAccountId}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new HttpRequestException($"Http Error {response.StatusCode} : {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetInternalBasketResponse>(json);
        }


    }
}
