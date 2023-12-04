using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory, 2));

        foreach (var line in input.LinesAsEnumerable())
        {
            var parsedLine = InputParser.Parse(line);
            Console.WriteLine(parsedLine);
        }

        Console.WriteLine("");
        return input;
    }

}
