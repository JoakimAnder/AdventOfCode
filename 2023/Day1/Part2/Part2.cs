using Infra.Helpers;
using Infra.Interfaces;
using System.Text.RegularExpressions;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => 54845;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part2)));

        var sum = 0;
        foreach (var line in input.LinesAsEnumerable())
        {
            var calibrationValue = ParseCalibrationValue(line);
            sum += calibrationValue;
        }

        Console.WriteLine("The sum of all of the calibration values is {0}", sum);
        return sum;
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

        var firstDigit = NumberLookup.TryGetValue(firstMatch.Value, out var fnum) ? fnum : Helper.ParseInt(firstMatch.ValueSpan);
        var lastDigit = NumberLookup.TryGetValue(lastMatchValue, out var lnum) ? lnum : Helper.ParseInt(lastMatch.ValueSpan);

        return Helper.ParseInt($"{firstDigit}{lastDigit}");

    }
}
