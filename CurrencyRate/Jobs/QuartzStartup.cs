using System;
using CurrencyRate.ConfigSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace CurrencyRate.Jobs
{
    public class QuartzStartup
    {
        private readonly IOptionsSnapshot<SchedulerSettings> _options;
        private readonly IServiceScope _scope;
        private IScheduler _scheduler;

        public QuartzStartup(IOptionsSnapshot<SchedulerSettings> options, IServiceProvider container)
        {
            _options = options;
            _scope = container.CreateScope();
        }

        public void Start()
        {
            if (_scheduler != null)
            {
                throw new InvalidOperationException("Already started.");
            }

            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = _scope.ServiceProvider.GetService<IJobFactory>();
            _scheduler.Start().Wait();

            var pollRatesJob = JobBuilder.Create<PollRatesJob>().Build();

            var pollRatesJobTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(_options.Value.ScheduleInterval).RepeatForever())
                .Build();

            _scheduler.ScheduleJob(pollRatesJob, pollRatesJobTrigger).Wait();
        }

        public void Stop()
        {
            if (_scheduler?.Shutdown(true).Wait(30000) == true)
            {
                _scheduler = null;
            }
        }
    }
}
