using System;
using System.Linq;

public static class StringExtensions
{
    public static string RemoveNewLinesAndAddSpace(this string text)
    {
        var sentence = text.Replace("\n", " ");
        return string.Join(" ", sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
    }

    public static int GetWhiteSpaceCount(this string text)
    {
        return text.Where(char.IsWhiteSpace).Count();
    }
}
