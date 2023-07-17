namespace ChainCalculations;

public interface IEnergyPriceProvider
{
    decimal GetPrice(EnergyKind kind, DateTime date);
    IEnumerable<DateTime> GetPriceChanges(DateTime from, DateTime to);
}
