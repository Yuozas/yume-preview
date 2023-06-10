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
}
