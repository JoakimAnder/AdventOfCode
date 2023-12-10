using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));

        var sum = 0;
        foreach (var line in input.LinesAsEnumerable())
        {
            var parsedLine = InputParser.Parse(line);
            var extrapolatedValue = parsedLine.ExtrapolatedValue();
            sum += extrapolatedValue;
        }

        Console.WriteLine("The sum of the extrapolated values is {0}", sum);
        return input;
    }

}
