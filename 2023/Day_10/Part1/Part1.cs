using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(GetType().Name));

        var image = InputParser.Parse(input);
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

        Console.WriteLine("The sum of these lengths is {0}", sum);
        return sum;
    }

}
