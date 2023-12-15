using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(GetType().Name));

        var parsed = InputParser.Parse(input);

        Console.WriteLine("");
        return parsed;
    }

}
