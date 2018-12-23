using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace CurrencyRate.Jobs
{
    public class PollRatesJobFactory : IJobFactory
    {
        private readonly IServiceScope _scope;

        public PollRatesJobFactory(IServiceProvider container)
        {
            _scope = container.CreateScope();
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _scope.ServiceProvider.GetService<IJob>();
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}
