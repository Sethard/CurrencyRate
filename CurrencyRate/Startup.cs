using System;
using System.Configuration;
using CurrencyRate.ConfigSettings;
using CurrencyRate.Converter;
using CurrencyRate.Entities;
using CurrencyRate.Jobs;
using CurrencyRate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Spi;

namespace CurrencyRate
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<ConnectionStrings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<SchedulerSettings>(_configuration.GetSection("SchedulerSettings"));
            services.Configure<CurrencyRateSettings>(_configuration.GetSection("CurrencyRateSettings"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IRateReaderService, RateReaderService>();
            services.AddTransient<IRateRepositoryService, RateRepositoryService>();
            services.AddScoped<IJob, PollRatesJob>();
            services.AddScoped<IJobFactory, PollRatesJobFactory>();
            services.AddTransient<IConverter<Models.CurrencyRate, CurrencyRateEntity>, ModelToEntityConverter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            IServiceProvider container,
            IOptionsSnapshot<SchedulerSettings> options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => routes.MapRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}"));

            var quartz = new QuartzStartup(options, container);
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopped.Register(quartz.Stop);
        }
    }
}
