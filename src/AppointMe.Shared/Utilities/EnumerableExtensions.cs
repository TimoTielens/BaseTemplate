namespace AppointMe.Shared.Utilities;

public static class EnumerableExtensions
{
    public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
        this IEnumerable<TLeft> left,
        IEnumerable<TRight> right,
        Func<TLeft, TKey> leftKeySelector,
        Func<TRight, TKey> rightKeySelector,
        Func<TLeft?, TRight?, TResult> resultSelector)
        where TKey : notnull
        where TLeft : class
        where TRight : class
    {
        var leftByKey = left.ToDictionary(leftKeySelector);
        var rightByKey = right.ToDictionary(rightKeySelector);

        foreach (var key in leftByKey.Keys.Union(rightByKey.Keys))
        {
            leftByKey.TryGetValue(key, out var leftItem);
            rightByKey.TryGetValue(key, out var rightItem);

            yield return resultSelector(leftItem, rightItem);
        }
    }
}
