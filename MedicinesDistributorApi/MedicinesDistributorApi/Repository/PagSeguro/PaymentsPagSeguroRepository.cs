using MedicinesDistributorApi.PagSeguro;
using MedicinesDistributorApi.Repository.IRepository.IPagSeguro;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MedicinesDistributorApi.Repository.PagSeguro
{
    public class PaymentsPagSeguroRepository : IPaymentsPagSeguroRepository
    {
        private readonly IConfiguration _configuration;
            
        public PaymentsPagSeguroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BoletoResponse> CreatePayment(BoletoRequest boletoRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = JsonConvert.SerializeObject(boletoRequest);
                    var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _configuration["PagSeguroBoletoUrlBearerToken"]);
                    HttpResponseMessage response = await client.PostAsync(_configuration["PagSeguroBoletoUrl"], bodyContent);
                    if (response != null)
                    {
                        string responseResult = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception(responseResult);
                        }
                        else
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var boletoResponse = JsonConvert.DeserializeObject<BoletoResponse>(responseBody);
                            return boletoResponse;
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
            return new BoletoResponse();
        }
    }
}
