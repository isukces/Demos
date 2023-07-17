namespace ChainCalculations;

public class CachedPowerConsumptionDataProvider : IPowerConsumptionDataProvider
{
    private CachedPowerConsumptionDataProvider(IPowerConsumptionDataProvider parent)
    {
        _cache = AutoAddDictionary.Create<Key, IReadOnlyList<DataSample>>(key =>
        {
            return parent.GetEnergyData(key.PersonId, key.Begin, key.End);
        });
    }

    public static IPowerConsumptionDataProvider MakeCache(IPowerConsumptionDataProvider provider)
    {
        if (provider is CachedPowerConsumptionDataProvider)
            return provider;
        return new CachedPowerConsumptionDataProvider(provider);
    }

    public IReadOnlyList<DataSample> GetEnergyData(int personId, DateTime begin, DateTime end)
    {
        return _cache.GetOrCreate(new Key(personId, begin, end));
    }

    private readonly IAutoAddDictionary<Key, IReadOnlyList<DataSample>> _cache;

    private record Key(int PersonId, DateTime Begin, DateTime End);
}
