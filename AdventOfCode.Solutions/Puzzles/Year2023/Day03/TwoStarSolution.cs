
using AdventOfCode.Solutions.Models;
using static AdventOfCode.Solutions.Puzzles.Year2023.Day03.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day03;

public class TwoStarSolution : ISolution
{
    private const char GearSymbol = '*';

    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var lines = Parse(input);
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

        return ValueTask.FromResult<object>(sum);
    }

    private static int CalculateGearRatio(Point2D gearPlacement, IEnumerable<SchematicNumber> numbers)
    {
        const int gearConfirmationLimit = 2;
        var adjacentNumbers = new List<SchematicNumber>();

        foreach (var number in numbers)
        {
            if (SchematicNumber.IsNextToAny(gearPlacement, number.Placement))
                adjacentNumbers.Add(number);

            if (adjacentNumbers.Count > gearConfirmationLimit)
                return 0;
        }

        if (adjacentNumbers.Count < gearConfirmationLimit)
            return 0;

        return adjacentNumbers.Select(n => n.Value).Aggregate((n1, n2) => n1 * n2);
    }
}
