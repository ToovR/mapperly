namespace Riok.Mapperly.Helpers;

public static class EnumerableExtensions
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable)
        where T : struct
    {
#nullable disable
        return enumerable.Where(x => x != null).Select(x => x.Value);
#nullable restore
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable)
        where T : notnull
    {
#nullable disable
        return enumerable.Where(x => x != null);
#nullable restore
    }

    public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> enumerable)
    {
        using var enumerator = enumerable.GetEnumerator();
        if (!enumerator.MoveNext())
            yield break;

        var previousItem = enumerator.Current;
        while (enumerator.MoveNext())
        {
            yield return previousItem;
            previousItem = enumerator.Current;
        }
    }

    public static TAccumulate AggregateWithPrevious<T, TAccumulate>(
        this IEnumerable<T> source,
        TAccumulate? seed,
        Func<TAccumulate?, T?, T, TAccumulate> func
    )
    {
        var result = seed;
        T? prev = default;
        foreach (var element in source)
        {
            result = func(result, prev, element);
            prev = element;
        }

        return result ?? throw new InvalidOperationException("Aggregation was not initialized");
    }

    public static IEnumerable<IEnumerable<TType>> GetCombinations<TType>(this IEnumerable<TType> list)
        where TType : IComparable
    {
        return Enumerable.Range(1, list.Count()).SelectMany(length => GetCombinations(list, length));
    }

    private static IEnumerable<IEnumerable<TType>> GetCombinations<TType>(IEnumerable<TType> list, int length)
        where TType : IComparable
    {
        if (length == 1)
            return list.Select(t => new TType[] { t });
        if (length == list.Count())
            return [list];
        return GetCombinations(list, length - 1).SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0), (t1, t2) => t1.Concat([t2]));
    }
}
