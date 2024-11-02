
using AdventOfCode.Solutions.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day03;

public partial class OneStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var lines = Parse(input);

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

        return ValueTask.FromResult<object>(sum);
    }


    [GeneratedRegex(@"\d+|[^.\n]")]
    private static partial Regex SchematicItemRegex();

    public static (SchematicNumber[] numbers, SchematicSymbol[] symbols)[] Parse(string input)
        => input
        .Split(Environment.NewLine)
        .Select(ParseItems)
        .ToArray();

    private static (SchematicNumber[] numbers, SchematicSymbol[] symbols) ParseItems(string input, int index)
    {
        var r = SchematicItemRegex();
        var items = r.Matches(input)
            .Select(m => ParseItems(m, index))
            .ToArray();

        var numbers = items
            .Select(i => i!.number)
            .OfType<SchematicNumber>()
            .ToArray();

        var symbols = items
            .Select(i => i.symbol)
            .OfType<SchematicSymbol>()
            .ToArray();

        return (numbers, symbols);
    }

    private static (SchematicNumber? number, SchematicSymbol? symbol) ParseItems(Match match, int index)
    {
        if (int.TryParse(match.ValueSpan, out var schNum))
        {
            var points = Enumerable
                .Range(match.Index, match.Length)
                .Select(x => new Point2D(x, index))
                .ToArray();
            return (number: new SchematicNumber(schNum, points), null);
        }

        return (null, symbol: new SchematicSymbol(match.ValueSpan[0], new Point2D(match.Index, index)));
    }

    public readonly record struct SchematicSymbol(char Value, Point2D Placement);
    public readonly record struct SchematicNumber(int Value, IEnumerable<Point2D> Placement)
    {
        public bool IsMachinePart(Point2D[] symbols)
            => Placement.Any(p => IsNextToAny(p, symbols));

        public static bool IsNextToAny(Point2D point, IEnumerable<Point2D> numberPlacement)
        {
            var minX = point.X - 1;
            var maxX = point.X + 1;
            return numberPlacement.Any(s => s.X >= minX && s.X <= maxX);
        }

    }
}
