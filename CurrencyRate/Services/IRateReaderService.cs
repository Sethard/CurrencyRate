namespace CurrencyRate.Services
{
    public interface IRateReaderService
    {
        Models.CurrencyRate GetRate(string url);
    }
}
