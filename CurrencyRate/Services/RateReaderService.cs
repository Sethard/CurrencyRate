using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CurrencyRate.Services
{
    public class RateReaderService : IRateReaderService
    {
        public Models.CurrencyRate GetRate(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(new Uri(url), HttpCompletionOption.ResponseContentRead).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var currency = JObject.Parse(content)["base"]["840"]["0"].ToObject<Models.CurrencyRate>();
            return currency;
        }
    }
}
