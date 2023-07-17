namespace ChainCalculations;

public class CalculationSession
{
    public CalculationSession(DbPowerConsumptionDataProvider db)
    {
        _db              = db;
        EnergyPrices     = CachedEnergyPriceProvider.MakeCache(DbEnergyPriceProvider.Instance);
        PowerConsumption = CachedPowerConsumptionDataProvider.MakeCache(DbPowerConsumptionDataProvider.Instance);
    }

    public IReadOnlyList<DataSample> GetEnergyData(int personId, DateTime begin, DateTime end)
    {
        var key = new Key(personId, begin, end);
        if (_cache.TryGetValue(key, out var result))
        {
            Console.WriteLine("pobranie danych z cache");
            return result;
        }

        result = _db.GetEnergyData(personId, begin, end);
        return _cache[key] = result;
    }

    public IPowerConsumptionDataProvider PowerConsumption { get; }

    private readonly Dictionary<Key, IReadOnlyList<DataSample>> _cache = new();

    private readonly DbPowerConsumptionDataProvider _db;


    public IEnergyPriceProvider EnergyPrices;

    private record Key(int PersonId, DateTime Begin, DateTime End);
}
