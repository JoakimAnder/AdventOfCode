
using Day2.Classes;
using Infra.Helpers;

namespace Day2;

public static class Part1
{
    public static void Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));
        var constraints = new Dictionary<CubeColor, int> {
            { CubeColor.Red, 12 },
            { CubeColor.Green, 13 },
            { CubeColor.Blue, 14 },
        };

        var sum = 0;

        foreach (var line in input.LinesAsEnumerable())
        {
            var game = InputParser.ParseGame(line);
            var points = game.CalculatePoints(constraints);
            sum += points;
        }

        Console.WriteLine("The sum of the IDs of those games is {0}", sum);
    }






}
