using Infra.Helpers;
using Infra.Interfaces;
using Models;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(GetType().Name));

        var strings = InputParser.Parse(input);
        var sum = strings.Select(s => HolidayAsciiStringHelperAlgorithm.Hash(s))
            .Sum();

        Console.WriteLine("The sum of the results is {0}", sum);
        return sum;
    }

}
