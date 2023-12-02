﻿
using Day2.Classes;
using Infra.Helpers;

namespace Day2;

public static class Part2
{
    public static void Run()
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
    }

}
