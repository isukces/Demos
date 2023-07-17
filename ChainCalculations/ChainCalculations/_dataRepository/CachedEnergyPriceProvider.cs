namespace ChainCalculations;

public class CachedEnergyPriceProvider : IEnergyPriceProvider
{
    private CachedEnergyPriceProvider(IEnergyPriceProvider parent)
    {
        var parentCopy = parent;
        _cacheGetPriceChanges = AutoAddDictionary.Create<KeyGetPriceChanges, IEnumerable<DateTime>>(k =>
        {
            return parentCopy.GetPriceChanges(k.From, k.To).ToArray();
        });
        _cacheGetPrice = AutoAddDictionary.Create<KeyGetPrice, decimal>(k =>
        {
            return parentCopy.GetPrice(k.Kind, k.Date);
        });
    }

    public static IEnergyPriceProvider MakeCache(IEnergyPriceProvider provider)
    {
        if (provider is CachedEnergyPriceProvider)
            return provider;
        return new CachedEnergyPriceProvider(provider);
    }


    public decimal GetPrice(EnergyKind kind, DateTime date)
    {
        return _cacheGetPrice.GetOrCreate(new KeyGetPrice(kind, date));
    }

    public IEnumerable<DateTime> GetPriceChanges(DateTime from, DateTime to)
    {
        return _cacheGetPriceChanges.GetOrCreate(new KeyGetPriceChanges(from, to));
    }

    private readonly IAutoAddDictionary<KeyGetPrice, decimal> _cacheGetPrice;
    private readonly IAutoAddDictionary<KeyGetPriceChanges, IEnumerable<DateTime>> _cacheGetPriceChanges;

    private record KeyGetPrice(EnergyKind Kind, DateTime Date);

    private record KeyGetPriceChanges(DateTime From, DateTime To);
}
