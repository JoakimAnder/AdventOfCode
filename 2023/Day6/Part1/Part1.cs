using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 2756160;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));

        var product = 1L;
        var races = InputParser.Parse(input.LinesAsEnumerable());
        foreach (var race in races)
        {
            var (min, max) = race.FindWinningChargeTimes();
            var winCount = max - min + 1;
            //Console.WriteLine("Race {0}, {1} - {2} ({3})", product, min, max, winCount);
            if (winCount > 0)
            {
                product *= winCount;
            }
        }

        Console.WriteLine("You get {0} if you multiply those numbers together", product);
        return product;
    }



}
