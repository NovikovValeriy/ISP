using _253504_Novikov_Lab1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _253504_Novikov_Lab1.Services
{
    public class RateService : IRateService
    {
        private readonly HttpClient _httpClient;
        private Dictionary<string, double> rates;
        public RateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            rates = new Dictionary<string, double>()
            {
                { "RUB", -1 },
                { "USD", -1 },
                { "EUR", -1 },
                { "CNY", -1 },
                { "GBP", -1 },
                { "CHF", -1 }
            };
        }

        public async Task<IEnumerable<Rate>> GetRates(DateTime date)
        {
            var list = new List<Rate>();
            foreach (var rate in rates)
            {
                var uri = new Uri(_httpClient.BaseAddress, $"{rate.Key}?parammode=2&ondate={date.ToString("yyy-MM-dd")}");
                try
                {
                    var response = await _httpClient.GetAsync(uri);
                    if(response.IsSuccessStatusCode) list.Add(JsonConvert.DeserializeObject<Rate>(await response.Content.ReadAsStringAsync()));
                }
                catch
                {

                }
            }
            return list;
        }
    }
}
