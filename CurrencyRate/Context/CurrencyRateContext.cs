using CurrencyRate.ConfigSettings;
using CurrencyRate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CurrencyRate.Context
{
    public class CurrencyRateContext : DbContext
    {
        private readonly IOptionsSnapshot<ConnectionStrings> _options;
        public DbSet<CurrencyRateEntity> CurrencyRates { get; set; }

        public CurrencyRateContext(IOptionsSnapshot<ConnectionStrings> options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _options.Value.DefaultConnection;
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}
