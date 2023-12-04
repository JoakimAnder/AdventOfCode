
using Day4.Classes;
using Infra.Helpers;
using Infra.Interfaces;

namespace Day4;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));

        var sum = 0;

        foreach (var line in input.LinesAsEnumerable())
        {
            var card = InputParser.ParseCard(line);
            var points = card.CalculatePoints();
            sum += points;
        }

        Console.WriteLine("The cards are worth {0} points in total.", sum);
        return sum;
    }

}
