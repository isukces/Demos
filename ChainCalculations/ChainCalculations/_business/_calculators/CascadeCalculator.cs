namespace ChainCalculations;

public class CascadeCalculator : ICalculator
{
    public void Calculate(DocumentSession session)
    {
        foreach (var i in Nested)
            i.Calculate(session);
    }

    public List<ICalculator> Nested { get; } = new();
}
