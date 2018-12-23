using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyRate.ConfigSettings;
using CurrencyRate.Context;
using CurrencyRate.Converter;
using CurrencyRate.Entities;
using Microsoft.Extensions.Options;

namespace CurrencyRate.Services
{
    public class RateRepositoryService : IRateRepositoryService
    {
        private readonly IOptionsSnapshot<ConnectionStrings> _options;
        private readonly IConverter<Models.CurrencyRate, CurrencyRateEntity> _currencyRateConverter;

        public RateRepositoryService(IOptionsSnapshot<ConnectionStrings> options, IConverter<Models.CurrencyRate, CurrencyRateEntity> currencyRateConverter)
        {
            _options = options;
            _currencyRateConverter = currencyRateConverter;
        }
        public IEnumerable<CurrencyRateEntity> GetRates()
        {
            using (var context = new CurrencyRateContext(_options))
            {
                context.Database.EnsureCreated();

                return context.CurrencyRates.ToList();
            }
        }

        public void AddRate(Models.CurrencyRate rate)
        {
            using (var context = new CurrencyRateContext(_options))
            {
                context.Database.EnsureCreated();
                var rateEntity = _currencyRateConverter.Convert(rate);
                context.CurrencyRates.Add(rateEntity);

                context.SaveChanges();
            }
        }
    }
}
