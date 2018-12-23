using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using CurrencyRate.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyRates.UnitTests.Services
{
    [TestClass]
    public class RateRepositoryServiceTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
            //_target = new RateRepositoryService();
        }

        [TestMethod]
        public void AddRateTest()
        {
            var currencyRate = _fixture.Build<CurrencyRate.Models.CurrencyRate>().Without(x => x.Id).Create();
            _target.AddRate(currencyRate);
        }

        [TestMethod]
        public void GetRatesTest()
        {
            var currencyRate = _fixture.Build<CurrencyRate.Models.CurrencyRate>().Without(x => x.Id).Create();
            _target.AddRate(currencyRate);

            var rates = _target.GetRates();

            Assert.IsNotNull(rates);
            Assert.AreNotEqual(0, rates.Count(), "At least 1 currency rate should be received");
        }

        private IRateRepositoryService _target;
        private IFixture _fixture;
    }
}
