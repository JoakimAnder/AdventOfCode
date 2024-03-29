﻿using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 21558;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));

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
