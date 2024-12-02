using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Helpers;
public static class SpanExtentions
{
    private static readonly Regex NumberRegex = Regexes.Number();
    public static IReadOnlyCollection<int> GetIntegers(this ReadOnlySpan<char> span)
    {
        List<int> ints = [];
        foreach (var match in NumberRegex.EnumerateMatches(span))
        {
            var range = match.ToRange();
            var value = span[range];
            if (int.TryParse(value, out int number))
                ints.Add(number);
        }

        return ints;
    }

    private static Range ToRange(this ValueMatch match)
        => new(match.Index, match.Index + match.Length);
}
