using CurrencyRate.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyRates.UnitTests.Services
{
    [TestClass]
    public class RateReaderServicesTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _target = new RateReaderService();
        }

        [TestMethod]
        public void GetRatesTest()
        {
            var currencyRate = _target.GetRate(RateUrl);
            Assert.IsNotNull(currencyRate, "Null object returned");
        }

        private const string RateUrl = "https://www.sberbank.ru/portalserver/proxy/?pipe=shortCachePipe&url=http%3A%2F%2Flocalhost%2Frates-web%2FrateService%2Frate%2Fcurrent%3FregionId%3D77%26rateCategory%3Dbase%26currencyCode%3D840";

        private IRateReaderService _target;
    }
}
