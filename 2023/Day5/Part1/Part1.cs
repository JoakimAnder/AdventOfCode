using Infra.Helpers;
using Infra.Interfaces;
using Models;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 650599855;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));

        var (seeds, maps) = InputParser.Parse(input.LinesAsEnumerable());

        var lowestLocationNumber = FindLowestLocationNumber(seeds, maps);

        Console.WriteLine("The lowest location number that corresponds to any of the initial seed numbers is {0}", lowestLocationNumber);
        return lowestLocationNumber;
    }

    private static long FindLowestLocationNumber(IEnumerable<long> values, IList<Map> maps)
    {
        var currentState = "seed";

        while (currentState != "location")
        {
            var map = maps.First(m => m.From == currentState);
            values = values.Select(map.MapValue).ToArray();
            currentState = map.To;
        }

        return values.Min();
    }
}
