namespace ChainCalculations;

public class RecomputeAgainException : Exception
{
    public RecomputeAgainException(string reasonWhy)
    {
        ReasonWhy = reasonWhy;
    }

    public string ReasonWhy { get; }
}
