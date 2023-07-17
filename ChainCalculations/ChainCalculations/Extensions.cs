namespace ChainCalculations;

public static class Extensions
{
    public static DateTime AddDays(this DateTime dateTime, Random random, double min, double max)
    {
        var days = random.Get(min, max);
        return dateTime.AddDays(days);
    }

    public static double Get(this Random random, double min, double max)
    {
        return random.NextDouble() * (max - min) + min;
    }
}
