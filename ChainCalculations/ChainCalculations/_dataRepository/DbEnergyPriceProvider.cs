namespace ChainCalculations;

public class DbEnergyPriceProvider : IEnergyPriceProvider
{
    private DbEnergyPriceProvider()
    {
        var     random = new Random(31415);
        decimal price  = 1;
        _priceChanges.Add(new PriceChange(new DateTime(1900, 1, 1), price));
        var d = new DateTime(2022, 1, 1).AddDays(random, 10, 40).Date;
        while (d.Year < 2024)
        {
            price += (decimal)random.Get(0.1, 0.3);
            price =  Math.Round(price, 2);

            d = d.AddDays(random, 10, 40).Date;
            _priceChanges.Add(new PriceChange(d, price));
        }
    }

    public decimal GetPrice(EnergyKind kind, DateTime date)
    {
        var last = _priceChanges.First().Price;
        foreach (var i in _priceChanges)
        {
            if (i.Date > date)
                return last;
            last = i.Price;
        }

        return last;
    }

    public IEnumerable<DateTime> GetPriceChanges(DateTime from, DateTime to)
    {
        return _priceChanges.Where(i => i.Date >= from && i.Date <= to).Select(i => i.Date);
    }

    public static DbEnergyPriceProvider Instance => EnergyPriceProviderHolder.SingleIstance;

    private readonly List<PriceChange> _priceChanges = new();

    private record PriceChange(DateTime Date, decimal Price);

    private static class EnergyPriceProviderHolder
    {
        public static readonly DbEnergyPriceProvider SingleIstance = new DbEnergyPriceProvider();
    }
}
