using System;
using CurrencyRate.Entities;

namespace CurrencyRate.Converter
{
    public sealed class ModelToEntityConverter : IConverter<Models.CurrencyRate, CurrencyRateEntity>
    {
        public CurrencyRateEntity Convert(Models.CurrencyRate source)
        {
            return new CurrencyRateEntity
            {
                BuyValue = source.BuyValue,
                SellValue = source.SellValue,
                CreationTimestamp = source.ActiveFrom
            };
        }
    }
}
