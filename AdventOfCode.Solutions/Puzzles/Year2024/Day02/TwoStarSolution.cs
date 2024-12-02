using System.Collections.Immutable;
using static AdventOfCode.Solutions.Puzzles.Year2024.Day02.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day02;

public class TwoStarSolution : ISolution
{
    private const int MIN_DIFFERENCE = 1;
    private const int MAX_DIFFERENCE = 3;

    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        const int BAD_LEVEL_TOLERANCE = 1;
        var reports = ParseInput(input);
        var safeReportCount = reports.Count(r => IsSafe(r, BAD_LEVEL_TOLERANCE));
        return ValueTask.FromResult<object>(safeReportCount);
    }

    private static bool IsSafe(Report report, int badLevelTolerance)
    {
        if (badLevelTolerance < 0)
            return false;
        badLevelTolerance--;

        var diffs = report.Levels.Zip(report.Levels.Skip(1), (l1, l2) => l2 - l1).ToImmutableArray();
        bool isReportDescending = diffs.Sum() < 0;

        foreach (var (diff, i) in diffs.Select((d, i) => (d, i)))
        {
            var isCurrentDiffDescending = diff < 0;
            if (isReportDescending != isCurrentDiffDescending)
                return IsSafe(report.WithoutLevelIndex(i), badLevelTolerance) || IsSafe(report.WithoutLevelIndex(i + 1), badLevelTolerance);

            var absDiff = Math.Abs(diff);
            if (absDiff < MIN_DIFFERENCE || absDiff > MAX_DIFFERENCE)
                return IsSafe(report.WithoutLevelIndex(i), badLevelTolerance) || IsSafe(report.WithoutLevelIndex(i + 1), badLevelTolerance);
        }

        return true;
    }
}

internal static class ReportExtentions
{
    public static Report WithoutLevelIndex(this Report report, int index)
    {
        var levels = report.Levels
            .Take(index)
            .Concat(report.Levels.Skip(index + 1))
            .ToArray();
        return new(levels);
    }
}