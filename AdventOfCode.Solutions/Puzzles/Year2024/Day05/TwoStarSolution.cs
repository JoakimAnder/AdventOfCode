using AdventOfCode.Solutions.Models;
using static AdventOfCode.Solutions.Puzzles.Year2024.Day05.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day05;

public class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var sum = 0;

        var (orderingRules, pages) = ParseInput(input);
        var pageComparer = new PageComparer(orderingRules);
        foreach (var page in pages)
        {
            var currentPage = page;
            if (IsInCorrectOrder(page, orderingRules))
                continue;

            var middlePage = page.Order(pageComparer).ElementAt(page.Count / 2);
            sum += middlePage;
        }

        return ValueTask.FromResult<object>(sum);
    }

    private class PageComparer(IReadOnlyCollection<Point2D> orderingRules) : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var rule = orderingRules.First(r => (r.X, r.Y) == (x, y) || (r.X, r.Y) == (y, x));

            if (x == rule.X)
                return -1;

            return 1;
        }
    }
}
