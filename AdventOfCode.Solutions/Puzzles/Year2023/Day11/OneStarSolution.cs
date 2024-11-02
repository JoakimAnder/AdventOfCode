
using AdventOfCode.Solutions.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day11;

public partial class OneStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var galaxies = input
            .Split(Environment.NewLine)
            .SelectMany(ParseLine);

        var image = new Image(galaxies);
        var expandedImage = image.Expand(2);

        var sum = 0;
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


    [GeneratedRegex(@"#")]
    public static partial Regex GalaxyRegex();

    internal static IEnumerable<Galaxy> ParseLine(string line, int y)
        => GalaxyRegex().Matches(line)
        .Select(m => new Galaxy((m.Index, y)));


    public readonly record struct Image(IEnumerable<Galaxy> Galaxies)
    {
        public readonly Image Expand(int expansion)
        {
            var populatedXSpaces = Galaxies
                .Select(g => g.Location.X)
                .Distinct()
                .Order()
                .ToArray();
            var populatedYSpaces = Galaxies
                .Select(g => g.Location.Y)
                .Distinct()
                .Order()
                .ToArray();

            var unpopulatedXSpaces = FindSpacesBetween(populatedXSpaces);
            var unpopulatedYSpaces = FindSpacesBetween(populatedYSpaces);

            var newGalaxies = Galaxies
                .Select(g =>
                {
                    var xExpantion = unpopulatedXSpaces.Where(x => x.space < g.Location.X).Sum(x => x.size) * (expansion - 1);
                    var yExpantion = unpopulatedYSpaces.Where(y => y.space < g.Location.Y).Sum(y => y.size) * (expansion - 1);
                    return new Galaxy(g.Location + (xExpantion, yExpantion));
                })
                .ToArray();

            return new(newGalaxies);
        }

        private static List<(int space, int size)> FindSpacesBetween(int[] spaces)
        {
            var min = spaces.Min();
            var max = spaces.Max();
            int? size = null;
            int? space = null;
            var emptySpaces = new List<(int space, int size)>();

            for (var i = min; i <= max; i++)
            {
                if (spaces.Contains(i))
                {
                    if (size.HasValue && space.HasValue)
                    {
                        emptySpaces.Add((space.Value, size.Value));
                    }

                    size = null;
                    space = null;
                    continue;
                }

                space ??= i;
                size ??= 0;
                size++;
            }

            return emptySpaces;
        }
    }

    public readonly record struct Galaxy(Point2D Location)
    {
        public readonly int DistanceTo(Galaxy other)
        {
            var difference = (other.Location - Location).Absolute();
            var distance = difference.X + difference.Y;
            return distance;
        }
    }

}
