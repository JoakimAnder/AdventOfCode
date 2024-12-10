using System.Text.RegularExpressions;
using static AdventOfCode.Solutions.Puzzles.Year2024.Day04.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day04;

public partial class TwoStarSolution : ISolution
{
    private static readonly IOrderedEnumerable<char> SortedCharsToFind = new char[] { 'M', 'S' }.Order();
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var xMases = Solve(input);

        return ValueTask.FromResult<object>(xMases);
    }

    private static int Solve(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf(Environment.NewLine) + Environment.NewLine.Length;

        var xMases = 0;

        var regex = ARegex();
        foreach (var match in regex.EnumerateMatches(input))
        {
            var startPoint = IndexToPoint(match.Index, width);

            var nw = startPoint + (-1, -1);
            var se = startPoint + (1, 1);
            var ne = startPoint + (1, -1);
            var sw = startPoint + (-1, 1);

            var nwc = PointToChar(input, width, nw);
            var sec = PointToChar(input, width, se);
            var nec = PointToChar(input, width, ne);
            var swc = PointToChar(input, width, sw);

            if ((nwc, sec) != ('M', 'S') && (nwc, sec) != ('S', 'M'))
                continue;
            if ((nec, swc) != ('M', 'S') && (nec, swc) != ('S', 'M'))
                continue;

            xMases++;
        }

        return xMases;
    }


    [GeneratedRegex("A")]
    private static partial Regex ARegex();


}
