
using Infra.Helpers;
using Infra.Interfaces;

namespace Day6;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory, 2));

        foreach (var line in input.LinesAsEnumerable())
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("");
        return input;
    }

}
