﻿using Infra.Helpers;
using Infra.Interfaces;
using Models;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 525119;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));

        var lines = input.ParseLines(InputParser.ParseItems).ToArray();

        var sum = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            var numbers = lines[y].numbers;

            IEnumerable<SchematicSymbol> symbols = lines[y].symbols;
            if (y != 0)
                symbols = symbols.Union(lines[y - 1].symbols);
            if (y != lines.Length - 1)
                symbols = symbols.Union(lines[y + 1].symbols);

            var symbolPlacements = symbols.Select(s => s.Placement).ToArray();

            var machineParts = numbers.Where(n => n.IsMachinePart(symbolPlacements));
            sum += machineParts.Sum(mp => mp.Value);
        }

        Console.WriteLine("The sum of all of the part numbers in the engine schematic is {0}", sum);
        return sum;
    }

}
