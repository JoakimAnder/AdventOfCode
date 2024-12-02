
using AdventOfCode.Solutions.Helpers;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day02;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var reports = ParseInput(input);
        var safeReportCount = reports.Count(IsSafe);
        return ValueTask.FromResult<object>(safeReportCount);
    }

    private static bool IsSafe(Report report)
    {
        const int MIN_DIFFERENCE = 1;
        const int MAX_DIFFERENCE = 3;
        bool? isFirstDiffNegative = null;
        foreach (var (first, second) in report.Levels.Zip(report.Levels.Skip(1)))
        {
            var diff = first - second;
            var absDiff = Math.Abs(diff);
            if (absDiff < MIN_DIFFERENCE || absDiff > MAX_DIFFERENCE)
                return false;

            var isCurrentDiffNegative = diff < 0;
            isFirstDiffNegative ??= isCurrentDiffNegative;

            if (isFirstDiffNegative != isCurrentDiffNegative)
                return false;
        }

        return true;
    }

    internal static IReadOnlyCollection<Report> ParseInput(ReadOnlySpan<char> input)
    {
        List<Report> reports = [];
        foreach (var line in input.EnumerateLines())
        {
            var levels = line.GetIntegers();
            reports.Add(new(levels));
        }
        return reports;
    }

    internal readonly partial record struct Report(IReadOnlyCollection<int> Levels);
}
