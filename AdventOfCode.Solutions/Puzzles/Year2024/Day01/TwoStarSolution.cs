using System.Collections.Frozen;
using static AdventOfCode.Solutions.Puzzles.Year2024.Day01.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day01;

public class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var (left, right) = ParseInput(input);

        var occurances = right.GroupBy(i => i)
            .ToFrozenDictionary(g => g.Key, g => g.Count());

        var similarityScore = left.Sum(l => l * occurances.GetValueOrDefault(l, 0));
        return ValueTask.FromResult<object>(similarityScore);
    }

}
