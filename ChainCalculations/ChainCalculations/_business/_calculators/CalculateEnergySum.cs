namespace ChainCalculations;

public class CalculateEnergySum : ICalculator
{
    public void Calculate(DocumentSession session)
    {
        var productName = Kind == EnergyKind.ActiveEnergy ? "Energia czynna pobrana" : "Energia bierna";

        var document = session.ResultDocument;
        for (var index = document.Items.Count - 1; index >= 0; index--)
        {
            var i = document.Items[index];
            if (i.Name.StartsWith(productName, StringComparison.OrdinalIgnoreCase))
                document.Items.RemoveAt(index);
        }

        foreach (var timeRange in session.Periods.GetTimeRanges())
        {
            var quantity        = GetEnergy(session, timeRange);
            var price           = GetenergyPrice(session, timeRange);
            var productNameFull = $"{productName} {timeRange.Start:yyyy-MM-dd} - {timeRange.End:yyyy-MM-dd}";

            DocumentItem item = new DocumentItem(productNameFull, price, quantity, 23, price * quantity);
            document.Items.Add(item);
        }
    }

    private int GetEnergy(DocumentSession session, TimeRange timeRange)
    {
        // szybka implementacja, żeby nie zaciemniać przykładu
        // wartość jest losowana, ale zależy od rodzaju energii i długości okresu
        var factor = timeRange.ToTotalDays * (Kind == EnergyKind.ActiveEnergy ? 1 : 0.3);
        var value  = _fakeNumebers.Get(10, 20) * factor;
        return (int)value;
    }

    private decimal GetenergyPrice(DocumentSession session, TimeRange timeRange)
    {
        var d = timeRange.Center;
        return session.Session.EnergyPrices.GetPrice(Kind, d);
    }

    public EnergyKind Kind { get; set; }

    private readonly Random _fakeNumebers = new Random(314);
}

public enum EnergyKind
{
    ActiveEnergy,
    ReactiveEnergy,
}
