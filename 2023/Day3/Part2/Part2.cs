using Extentions;
using Infra.Helpers;
using Infra.Interfaces;
using Infra.Models;
using Models;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    private const char GearSymbol = '*';

    public object? ExpectedResult => 76504829;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part2)));

        var lines = input.ParseLines(InputParser.ParseItems).ToArray();

        var sum = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            var potentialGears = lines[y].symbols.Where(s => s.Value == GearSymbol)
                .Select(g => g.Placement);

            IEnumerable<SchematicNumber> numbers = lines[y].numbers;
            if (y != 0)
                numbers = numbers.Union(lines[y - 1].numbers);
            if (y != lines.Length - 1)
                numbers = numbers.Union(lines[y + 1].numbers);

            numbers = numbers.ToArray();

            var gearRatios = potentialGears.Select(g => CalculateGearRatio(g, numbers));
            sum += gearRatios.Sum();
        }

        Console.WriteLine("The sum of all of the gear ratios in your engine schematic is {0}", sum);
        return sum;
    }
    private static int CalculateGearRatio(Point2D gearPlacement, IEnumerable<SchematicNumber> numbers)
    {
        const int gearConfirmationLimit = 2;
        var adjacentNumbers = new List<SchematicNumber>();

        foreach (var number in numbers)
        {
            if (gearPlacement.IsNextToAny(number.Placement))
                adjacentNumbers.Add(number);

            if (adjacentNumbers.Count > gearConfirmationLimit)
                return 0;
        }

#pragma warning disable S2583 // Conditionally executed code should be reachable // hmmmm, I believe the count could be 2, which shouldn't be evaluated to True
        if (adjacentNumbers.Count < gearConfirmationLimit)
            return 0;
#pragma warning restore S2583 // Conditionally executed code should be reachable

        return adjacentNumbers.Select(n => n.Value).Aggregate((n1, n2) => n1 * n2);
    }


}
