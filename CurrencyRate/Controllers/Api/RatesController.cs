using System.Globalization;
using System.Linq;
using CurrencyRate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyRate.Controllers.Api
{
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
        private const string Template = @"({{
                chart: {{
                    type: 'line'
                }},
                title: {{
                    text: 'Dollar Rates To Ruble'
                }},
                subtitle: {{
                    text: 'Source: https://www.sberbank.ru/ru/quotes/currencies'
                }},
                xAxis: {{
                    categories: [{0}]
                }},
                yAxis: {{
                    title: {{
                        text: 'Rate (RUB)'
                    }}
                }},
                plotOptions: {{
                    line: {{
                        dataLabels: {{
                            enabled: true
                        }},
                        enableMouseTracking: true
                    }}
                }},
                series: [
                    {{
                        name: 'Buy',
                        data: [{1}]
                    }}, {{
                        name: 'Sell',
                        data: [{2}]
                    }}
                ]
            }})";

        private readonly IRateRepositoryService _rateRepositoryService;

        public RatesController(IRateRepositoryService rateRepositoryService)
        {
            _rateRepositoryService = rateRepositoryService;
        }
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            var rates = _rateRepositoryService.GetRates();
            var dates = rates.Select(x => $"'{x.CreationTimestamp.ToString(CultureInfo.InvariantCulture)}'").ToArray().Join(",");
            var buyValues = rates.Select(x => x.BuyValue.ToString(CultureInfo.InvariantCulture)).ToArray().Join(",");
            var sellValues = rates.Select(x => x.SellValue.ToString(CultureInfo.InvariantCulture)).ToArray().Join(",");
            return string.Format(Template, dates, buyValues, sellValues);
        }
    }
}
