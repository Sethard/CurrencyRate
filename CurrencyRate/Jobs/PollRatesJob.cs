using System.Threading.Tasks;
using CurrencyRate.ConfigSettings;
using CurrencyRate.Services;
using Microsoft.Extensions.Options;
using Quartz;

namespace CurrencyRate.Jobs
{
    public class PollRatesJob : IJob
    {
        private readonly IOptionsSnapshot<CurrencyRateSettings> _options;
        private readonly IRateReaderService _rateReaderService;
        private readonly IRateRepositoryService _rateRepositoryService;

        public PollRatesJob(IOptionsSnapshot<CurrencyRateSettings> options, IRateReaderService rateReaderService, IRateRepositoryService rateRepositoryService)
        {
            _options = options;
            _rateReaderService = rateReaderService;
            _rateRepositoryService = rateRepositoryService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                var settings = _options.Value;
                var rate = _rateReaderService.GetRate(settings.CurrencyRateRequestUrl);
                _rateRepositoryService.AddRate(rate);
            });
        }


    }
}
