namespace ChainCalculations;

/// <summary>
///     Kalkulator skanujący zmiany cen energii i dodający je do listy krawędzi czasowych.
/// </summary>
public class AddTimeEdgeBecauseOfPriceChange : ICalculator
{
    public void Calculate(DocumentSession session)
    {
        var periods   = session.Periods;
        var from      = periods.StartDate;
        var to        = periods.EndDate;
        var timeEdges = session.Session.EnergyPrices.GetPriceChanges(from, to).ToArray();

        var current          = periods.EdgeDates.ToHashSet();
        var isAnyNewEdgeDate = false;
        foreach (var time in timeEdges)
        {
            if (current.Add(time))
                isAnyNewEdgeDate = true;
        }

        if (!isAnyNewEdgeDate) return;
        session.Periods = periods.WithEdgeDates(current);
        throw new RecomputeAgainException("Dodano nowe krawędzie czasowe. Trzeba powtórzyć obliczenia.");
    }
}
