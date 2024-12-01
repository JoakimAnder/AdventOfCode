using static AdventOfCode.Solutions.Puzzles.Year2023.Day06.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day06;

public class TwoStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var product = 1L;
        var races = Parse(input.Replace(" ", null));
        foreach (var race in races)
        {
            var (min, max) = race.FindWinningChargeTimes();
            var winCount = max - min + 1;
            if (winCount > 0)
            {
                product *= winCount;
            }
        }

        return ValueTask.FromResult<object>(product);
    }

}
