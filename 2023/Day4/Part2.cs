
using Infra.Helpers;

namespace Day4;

public static class Part2
{
    public static void Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory, 2));

        foreach (var line in input.LinesAsEnumerable())
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("");
    }

}
