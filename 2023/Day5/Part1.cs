
using Infra.Helpers;
using Infra.Interfaces;

namespace Day5;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory, 1));

        foreach (var line in input.LinesAsEnumerable())
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("");
        return input;
    }

}
