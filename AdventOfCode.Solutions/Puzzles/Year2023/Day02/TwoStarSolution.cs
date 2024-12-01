
using static AdventOfCode.Solutions.Puzzles.Year2023.Day02.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day02;

public class TwoStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var sum = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            var game = ParseGame(line.ToString());
            var power = CalculatePower(game);
            sum += power;
        }

        return ValueTask.FromResult<object>(sum);
    }

    private static int CalculatePower(Game game)
    {
        var minimum = GetMinimumPossible(game);
        var power = 1;
        foreach (var num in minimum.Values)
        {
            power *= num;
        }
        return power;
    }

    private static Dictionary<CubeColor, int> GetMinimumPossible(Game game)
        => game.Draws
            .SelectMany(d => d)
            .GroupBy(d => d.Key, (color, draws) => (
                color,
                draws.Select(d => d.Value).Max()
            ))
            .ToDictionary();
}
