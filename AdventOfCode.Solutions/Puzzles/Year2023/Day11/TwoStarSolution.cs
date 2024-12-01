
using static AdventOfCode.Solutions.Puzzles.Year2023.Day11.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day11;

public class TwoStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var galaxies = input
            .Split(Environment.NewLine)
            .SelectMany(ParseLine);

        var image = new Image(galaxies);
        var expandedImage = image.Expand(1000000);

        var sum = 0L;
        for (var i = 0; i < expandedImage.Galaxies.Count(); i++)
        {
            var currentGalaxy = expandedImage.Galaxies.ElementAt(i);
            for (var j = i + 1; j < expandedImage.Galaxies.Count(); j++)
            {
                var nextGalaxy = expandedImage.Galaxies.ElementAt(j);
                var distance = currentGalaxy.DistanceTo(nextGalaxy);
                sum += distance;
            }
        }

        return ValueTask.FromResult<object>(sum);
    }
}
