using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyClient
{
    class CurrencyConsumer
    {
        private readonly HttpClient client = new HttpClient();
        private const string endPoint = "api/ExchangeRates?date={0}&rate={1}";
        private const string logStatus = "status:{0} [{1}]";

        public CurrencyConsumer(string url)
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async void PostCurrencyAsync(CurrencyEntry record)
        {
            // Build query string
            string queryString = String.Format(endPoint, record.date, record.rate);
            try
            {
                HttpResponseMessage response = await client.PostAsync(queryString, null);
                Console.WriteLine(
                    String.Format(logStatus,
                    (int)response.StatusCode, response.StatusCode));
            }
            catch (HttpRequestException e){
                Console.WriteLine("Error: " + e.Message.ToString());
            }
            
        }
                
    }
}