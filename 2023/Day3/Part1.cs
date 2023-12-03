
using Day3.Classes;
using Infra.Classes;
using Infra.Helpers;

namespace Day3;

public static partial class Part1
{
    public static void Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));

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

            var machineParts = numbers.Where(n => IsMachinePart(n, symbolPlacements));
            sum += machineParts.Sum(mp => mp.Value);
        }

        Console.WriteLine("The sum of all of the part numbers in the engine schematic is {0}", sum);
    }

    private static bool IsMachinePart(this SchematicNumber number, Point2D[] symbols) =>
        number.Placement.Any(p => p.IsNextToAny(symbols));

    private static bool IsNextToAny(this Point2D point, Point2D[] symbols)
    {
        var minX = point.X - 1;
        var maxX = point.X + 1;
        return Array.Exists(symbols, s => s.X >= minX && s.X <= maxX);
    }
}
