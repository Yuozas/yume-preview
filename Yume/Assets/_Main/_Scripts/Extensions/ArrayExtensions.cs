using System;
using System.Collections.Generic;
using System.Linq;

public static class ArrayExtensions
{
    /// <summary>Indicates whether the specified array is null or has a length of zero.</summary>
    /// <param name="array">The array to test.</param>
    /// <returns>True if the array parameter is null or has a length of zero, otherwise False.</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> array)
    {
        return array == null || !array.Any();
    }

    public static TResult? SelectFirstValid<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, TResult?> selector) where TResult : struct
    {
        foreach (var item in source)
        {
            var result = selector(item);
            if (result is not null)
                return result;
        }
        return null;
    }

    public static TResult SelectFirstValid<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, TResult> selector) where TResult : class
    {
        foreach (var item in source)
        {
            var result = selector(item);
            if (result is not null)
                return result;
        }
        return null;
    }

    public static int GetLastElementIndex<T>(this IEnumerable<T> source)
    {
        return source.Count() - 1;
    }
}
