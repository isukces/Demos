namespace ChainCalculations;

public interface IPowerConsumptionDataProvider
{
    IReadOnlyList<DataSample> GetEnergyData(int personId, DateTime begin, DateTime end);
}
