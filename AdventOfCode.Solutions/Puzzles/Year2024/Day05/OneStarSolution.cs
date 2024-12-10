using AdventOfCode.Solutions.Helpers;
using AdventOfCode.Solutions.Models;
using System.Collections.Frozen;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day05;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var sum = 0;

        var (orderingRules, pages) = ParseInput(input);

        foreach (var page in pages)
        {
            if (!IsInCorrectOrder(page, orderingRules))
                continue;

            var middlePage = page.ElementAt(page.Count / 2);
            sum += middlePage;
        }

        return ValueTask.FromResult<object>(sum);
    }

    internal static bool IsInCorrectOrder(IReadOnlyCollection<int> page, IReadOnlyCollection<Point2D> orderingRules)
    {
        var indexedPage = page.Index().ToFrozenDictionary(t => t.Item, t => t.Index);
        foreach (var rule in orderingRules)
        {
            if (!indexedPage.TryGetValue(rule.X, out var x) || !indexedPage.TryGetValue(rule.Y, out var y))
                continue;

            var isWrongOrder = x >= y;
            if (isWrongOrder)
                return false;
        }

        return true;
    }

    internal static (IReadOnlyCollection<Point2D> OrderingRules, IReadOnlyCollection<IReadOnlyCollection<int>> Pages) ParseInput(ReadOnlySpan<char> input)
    {
        List<Point2D> orderingRules = [];
        List<IReadOnlyCollection<int>> pages = [];

        foreach (var line in input.EnumerateLines())
        {
            if (line.Contains(','))
            {
                pages.Add(line.GetIntegers());
                continue;
            }

            if (line.Contains('|'))
            {
                var ints = line.GetIntegers();
                orderingRules.Add((ints.First(), ints.Last()));
            }

        }

        return (orderingRules, pages);
    }
}
