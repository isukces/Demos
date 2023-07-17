namespace ChainCalculations;

public static class AutoAddDictionary
{
    public static IAutoAddDictionary<TKey, TValue> Create<TKey, TValue>(Func<TKey, TValue> factory)
        where TKey : notnull
    {
        return new AutoAddDictionary<TKey, TValue>(factory);
    }
}

public class AutoAddDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IAutoAddDictionary<TKey, TValue>
    where TKey : notnull
{
    public AutoAddDictionary(Func<TKey, TValue> factory)
    {
        _factory = factory;
    }

    public virtual TValue? GetOrCreate(TKey key)
    {
        if (TryGetValue(key, out var value))
            return value;
        value = _factory(key);
        if (value is null)
            return value;
        return this[key] = value;
    }

    private readonly Func<TKey, TValue> _factory;
}

public interface IAutoAddDictionary<in TKey, out TValue>
{
    TValue GetOrCreate(TKey key);
}
