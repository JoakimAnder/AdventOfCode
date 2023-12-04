using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));
        var sum = 0;

        foreach (var line in input.LinesAsEnumerable())
        {
            var game = InputParser.ParseGame(line);
            var power = game.CalculatePower();
            sum += power;
        }

        Console.WriteLine("The sum of the powers of those sets is {0}", sum);
        return sum;
    }

}
