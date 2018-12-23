using CurrencyRate.ConfigSettings;
using Microsoft.Extensions.Options;

namespace CurrencyRate.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IOptionsSnapshot<CurrencyRateSettings> _options;
        private readonly IRateReaderService _rateReaderService;
        private readonly IRateRepositoryService _rateRepositoryService;

        public SchedulerService(IOptionsSnapshot<CurrencyRateSettings> options,IRateReaderService rateReaderService, IRateRepositoryService rateRepositoryService)
        {
            _options = options;
            _rateReaderService = rateReaderService;
            _rateRepositoryService = rateRepositoryService;
        }

        public void ScheduleRatesPoll()
        {
            var settings = _options.Value;
            var rate = _rateReaderService.GetRate(settings.CurrencyRateRequestUrl);
            _rateRepositoryService.AddRate(rate);
        }
    }
}
