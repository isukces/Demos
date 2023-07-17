namespace ChainCalculations;

public class DocumentSession
{
    public DocumentSession(CalculationSession session, Person person, DateTime startDate, DateTime endDate)
    {
        Session = session;
        Person  = person;
        Periods = new TimePeriods(startDate, endDate);
    }

    public CalculationSession Session        { get; }
    public Person             Person         { get; }
    public TimePeriods        Periods        { get; set; }
    public Document           ResultDocument { get; } = new();
}

public class TimePeriods
{
    public TimePeriods(DateTime startDate, DateTime endDate, IReadOnlyList<DateTime>? edgeDates = null)
    {
        StartDate = startDate;
        EndDate   = endDate;
        EdgeDates = edgeDates ?? Array.Empty<DateTime>();
    }

    public IEnumerable<TimeRange> GetTimeRanges()
    {
        var dates = new List<DateTime>();
        dates.Add(StartDate);
        dates.AddRange(EdgeDates);
        dates.Add(EndDate);
        dates = dates.Distinct().ToList();
        dates.Sort();
        for (var i = 1; i < dates.Count; i++)
        {
            yield return new TimeRange(dates[i - 1], dates[i]);
        }
    }

    public void Print()
    {
        Console.WriteLine("Przedziały czasowe obliczeń");
        foreach (var i in GetTimeRanges())
            Console.WriteLine();
    }

    public TimePeriods WithEdgeDates(IEnumerable<DateTime> current)
    {
        return new TimePeriods(StartDate, EndDate, current.OrderBy(a => a).ToArray());
    }

    public DateTime                StartDate { get; }
    public DateTime                EndDate   { get; }
    public IReadOnlyList<DateTime> EdgeDates { get; }
}

public record TimeRange(DateTime Start, DateTime End)
{
    public DateTime Center => Start.AddDays(ToTotalDays / 2);

    public double ToTotalDays => (End - Start).TotalDays;
}
