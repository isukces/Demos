namespace ChainCalculations;

public class DbPowerConsumptionDataProvider : IPowerConsumptionDataProvider
{
    private DbPowerConsumptionDataProvider()
    {
        People = new[]
        {
            new Person(1, "Jan Kowalski"),
            new Person(2, "Karol Malinowski"),
        };
        var random = new Random(314159265);
        foreach (var i in People)
        {
            var dt    = new DateTime(2023, 1, 1).AddDays(random.NextDouble() * 15);
            var value = random.NextDouble() * 5_000 + 5_000;
            while (dt.Year < 2024)
            {
                var entry = new DataSampleInDatabase(i.Id, dt, Math.Round((decimal)value, 1));
                _allData.Add(entry);
                dt    =  dt.AddDays(random.NextDouble() * 15 + 15);
                value += random.NextDouble() * 1_000 - 200;
            }
        }
    }

    public IReadOnlyList<DataSample> GetEnergyData(int personId, DateTime begin, DateTime end)
    {
        Console.WriteLine("pobranie danych ze źródła danych");
        return _allData
            .Where(x => x.PersonId == personId)
            .Where(x => x.ReadTimestamp >= begin)
            .Where(x => x.ReadTimestamp < end)
            .Select(x => new DataSample(x.ReadTimestamp, x.Value))
            .ToList();
    }

    public static DbPowerConsumptionDataProvider Instance => PowerConsumptionDatabaseHolder.SingleIstance;

    public IReadOnlyList<Person> People { get; }

    private readonly List<DataSampleInDatabase> _allData = new List<DataSampleInDatabase>();

    private static class PowerConsumptionDatabaseHolder
    {
        public static readonly DbPowerConsumptionDataProvider SingleIstance = new DbPowerConsumptionDataProvider();
    }
}

public record DataSample(DateTime ReadTimestamp, decimal Value);

public record DataSampleInDatabase(int PersonId, DateTime ReadTimestamp, decimal Value);

public record Person(int Id, string FullName);
