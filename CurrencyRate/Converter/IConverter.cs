namespace CurrencyRate.Converter
{
    public interface IConverter<in TFrom, out TTo>
    {
        TTo Convert(TFrom source);
    }
}
