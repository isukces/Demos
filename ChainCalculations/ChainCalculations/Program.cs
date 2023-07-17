using ChainCalculations;

// ================================
Initialize();
D01_Fetch_data_with_or_without_cache();
D02_Calculate_energy_sum();

// ================================

static void D01_Fetch_data_with_or_without_cache()
{
    var startDate = new DateTime(2023, 6, 1);
    var endDate   = new DateTime(2023, 7, 1);

    Console.WriteLine("* Pobieranie danych bez cache");
    IPowerConsumptionDataProvider db = DbPowerConsumptionDataProvider.Instance;
    for (var i = 0; i < 4; i++)
    {
        db.GetEnergyData(1, startDate, endDate);
    }

    Console.WriteLine("* Pobieranie danych z cache");

    db = CachedPowerConsumptionDataProvider.MakeCache(db);

    for (var i = 0; i < 4; i++)
    {
        db.GetEnergyData(1, startDate, endDate);
    }
}

void D02_Calculate_energy_sum()
{
    var calculator = new SampleCalculationSet();
    var database   = DbPowerConsumptionDataProvider.Instance;
    var session    = new CalculationSession(database);

    var startDate = new DateTime(2023, 6, 1);
    var endDate   = new DateTime(2023, 7, 1);

    foreach (var person in database.People)
    {
        var documentSession = new DocumentSession(session, person, startDate, endDate);
        calculator.Calculate(documentSession);
        Console.WriteLine("==================================");
        documentSession.Periods.Print();
        documentSession.ResultDocument.Print();
    }
}

static void Initialize()
{
}
