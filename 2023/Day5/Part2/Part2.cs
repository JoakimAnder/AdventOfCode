using Infra.Helpers;
using Infra.Interfaces;
using Models;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => 46L;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part2)));

        var (seeds, maps) = InputParser.Parse2(input.LinesAsEnumerable());

        var lowestLocationNumber = FindLowestLocationNumber(seeds, maps);

        Console.WriteLine("The lowest location number that corresponds to any of the initial seed numbers is {0}", lowestLocationNumber);
        return lowestLocationNumber;
    }
    private static long FindLowestLocationNumber(IEnumerable<MapRange> ranges, IList<Map> maps)
    {
        var currentState = "seed";

        while (currentState != "location")
        {
            var map = maps.First(m => m.From == currentState);
            ranges = ranges.SelectMany(map.MapRange).ToArray();
            currentState = map.To;
        }

        ranges = ranges.OrderBy(r => r.SourceStart).ToArray();
        return ranges.MinBy(r => r.SourceStart).SourceStart;
    }
}
