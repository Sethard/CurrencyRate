using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CurrencyRate
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddAppConfiguration)
                .UseStartup<Startup>();
        }

        public static void AddAppConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            config.AddJsonFile("appsettings.json", optional: true);
        }
    }
}
