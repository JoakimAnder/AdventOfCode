
using Infra.Helpers;

namespace Day7;

public static class Part1
{
    public static void Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory, 1));

        foreach (var line in input.LinesAsEnumerable())
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("");
    }

}
