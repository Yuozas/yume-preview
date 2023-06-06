using System;

public static class StringExtensions
{
    public static string RemoveNewLinesAndAddSpace(this string text)
    {
        var sentence = text.Replace("\n", " ");
        return string.Join(" ", sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
    }
}
