using AdventOfCode.Solutions.Helpers;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day01;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var (left, right) = ParseInput(input);

        var orderedLeft = left.Order();
        var orderedRight = right.Order();

        var totalDistance = orderedLeft.Zip(orderedRight).Sum(z => Math.Abs(z.First - z.Second));
        return ValueTask.FromResult<object>(totalDistance);
    }

    internal static (IReadOnlyCollection<int> left, IReadOnlyCollection<int> right) ParseInput(ReadOnlySpan<char> input)
    {
        List<int> left = [];
        List<int> right = [];
        var regex = Regexes.Number();
        foreach (var line in input.EnumerateLines())
        {
            if (line.Length == 0)
                continue;
            var numbers = regex.EnumerateMatches(line);
            numbers.MoveNext();
            var leftMatch = numbers.Current;
            numbers.MoveNext();
            var rightMatch = numbers.Current;

            var leftRange = new Range(leftMatch.Index, leftMatch.Index + leftMatch.Length);
            var rightRange = new Range(rightMatch.Index, rightMatch.Index + rightMatch.Length);
            var l = int.Parse(line[leftRange]);
            var r = int.Parse(line[rightRange]);
            left.Add(l);
            right.Add(r);
        }

        return (left, right);
    }


}
