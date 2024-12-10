using AdventOfCode.Solutions.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day04;

public partial class OneStarSolution : ISolution
{
    private const string WordToFind = "XMAS";
    private static readonly Point2D[] Directions = [
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 0),
        (1, 1),
        ];

    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var xmases = Solve(input);
        return ValueTask.FromResult<object>(xmases);
    }


    private static int Solve(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf(Environment.NewLine) + Environment.NewLine.Length;

        var xmases = 0;

        var regex = XRegex();
        foreach (var match in regex.EnumerateMatches(input))
        {
            var startPoint = IndexToPoint(match.Index, width);

            for (var directionIndex = 0; directionIndex < Directions.Length; directionIndex++)
            {
                var direction = Directions[directionIndex];
                var currentPoint = startPoint;

                for (var charIndex = 1; charIndex < WordToFind.Length; charIndex++)
                {
                    var charToMatch = WordToFind[charIndex];
                    currentPoint += direction;
                    var ch = PointToChar(input, width, currentPoint);

                    var isCharIncorrect = ch != charToMatch;
                    if (isCharIncorrect)
                        break;

                    var isLastChar = charToMatch == WordToFind[^1];
                    if (isLastChar)
                        xmases++;
                }
            }
        }

        return xmases;
    }

    internal static Point2D IndexToPoint(int i, int width)
        => (i % width, i / width);

    internal static int PointToIndex(Point2D point, int width)
        => point.X + point.Y * width;

    internal static char PointToChar(ReadOnlySpan<char> chars, int width, Point2D point)
    {
        var isPointOOB = point.X < 0 || point.X >= width || point.Y < 0;
        if (isPointOOB)
            return default;

        var index = PointToIndex(point, width);
        var isIndexOOB = index >= chars.Length;
        if (isIndexOOB)
            return default;

        return chars[index];
    }

    [GeneratedRegex("X")]
    private static partial Regex XRegex();
}
