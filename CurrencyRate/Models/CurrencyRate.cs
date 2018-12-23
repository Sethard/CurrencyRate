using System;
using CurrencyRate.JsonConverters;
using Newtonsoft.Json;

namespace CurrencyRate.Models
{
    public sealed class CurrencyRate
    {
        public long Id { get; set; }

        [JsonProperty(PropertyName = "isoCur")]
        public string IsoCur { get; set; }

        [JsonProperty(PropertyName = "currencyName")]
        public string CurrencyName { get; set; }

        [JsonProperty(PropertyName = "currencyNameEng")]
        public string CurrencyNameEn { get; set; }

        [JsonProperty(PropertyName = "rateType")]
        public string RateType { get; set; }

        [JsonProperty(PropertyName = "categoryCode")]
        public string CategoryCode { get; set; }

        [JsonProperty(PropertyName = "scale")]
        public int Scale { get; set; }

        [JsonProperty(PropertyName = "buyValue")]
        public decimal BuyValue { get; set; }

        [JsonProperty(PropertyName = "sellValue")]
        public decimal SellValue { get; set; }

        [JsonProperty(PropertyName = "activeFrom")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime ActiveFrom { get; set; }

        [JsonProperty(PropertyName = "buyValuePrev")]
        public decimal BuyValuePrev { get; set; }

        [JsonProperty(PropertyName = "sellValuePrev")]
        public decimal SellValuePrev { get; set; }

        [JsonProperty(PropertyName = "amountFrom")]
        public decimal AmountFrom { get; set; }

        [JsonProperty(PropertyName = "amountTo")]
        public decimal AmountTo { get; set; }

    }
}
