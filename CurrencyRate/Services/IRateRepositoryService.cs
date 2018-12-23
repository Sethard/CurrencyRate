using System.Collections.Generic;
using CurrencyRate.Entities;

namespace CurrencyRate.Services
{
    public interface IRateRepositoryService
    {
        IEnumerable<CurrencyRateEntity> GetRates();

        void AddRate(Models.CurrencyRate rate);
    }
}
