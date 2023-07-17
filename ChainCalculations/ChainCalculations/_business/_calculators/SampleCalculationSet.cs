namespace ChainCalculations;

/// <summary>
///     Zestaw obliczeniowy - zawiera definicje i powiązania kalkulatorów
/// </summary>
public class SampleCalculationSet : ICalculator
{
    public SampleCalculationSet()
    {
        _mySequence = new CascadeCalculator
        {
            Nested =
            {
                new AddTimeEdgeBecauseOfPriceChange(),
                new CalculateEnergySum
                {
                    Kind = EnergyKind.ActiveEnergy
                },
                new CalculateEnergySum
                {
                    Kind = EnergyKind.ReactiveEnergy
                },
                new AddDocumentHeader()
            }
        };
    }

    public void Calculate(DocumentSession session)
    {
        var again = true;
        while (again)
        {
            again = false;
            try
            {
                _mySequence.Calculate(session);
            }
            catch (RecomputeAgainException e)
            {
                again = true;
                Console.WriteLine(">> " + e.ReasonWhy);
            }
        }
    }

    private readonly CascadeCalculator _mySequence;
}
