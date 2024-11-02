
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day01;

public class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var sum = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;

            var calibrationValue = ParseCalibrationValue(line.ToString());
            sum += calibrationValue;
        }

        return ValueTask.FromResult<object>(sum);
    }

    private readonly static Dictionary<string, int> NumberLookup = new() {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    private static readonly Regex FirstRegex = new($"\\d|{string.Join('|', NumberLookup.Keys)}");
    private static readonly Regex LastRegex = new($"\\d|{string.Join('|', NumberLookup.Keys.Select(name => new string(name.Reverse().ToArray())))}");
    private static int ParseCalibrationValue(string line)
    {
        var firstMatch = FirstRegex.Match(line);
        var lastMatch = LastRegex.Match(new string(line.Reverse().ToArray()));
        var lastMatchValue = new string(lastMatch.Value.Reverse().ToArray());

        var firstDigit = NumberLookup.TryGetValue(firstMatch.Value, out var fnum) ? fnum : int.Parse(firstMatch.ValueSpan);
        var lastDigit = NumberLookup.TryGetValue(lastMatchValue, out var lnum) ? lnum : int.Parse(lastMatch.ValueSpan);

        return int.Parse($"{firstDigit}{lastDigit}");

    }

}
