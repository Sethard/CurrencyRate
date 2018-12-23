using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyRate.Entities
{
    [Table("currencyrate")]
    public class CurrencyRateEntity
    {
        public long Id { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public decimal BuyValue { get; set; }

        public decimal SellValue { get; set; }
    }
}
