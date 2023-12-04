using Infra.Models;
using Models;
using System.Text.RegularExpressions;

namespace Puzzle;

public static partial class InputParser
{

    [GeneratedRegex(@"\d+|[^.\n]")]
    private static partial Regex SchematicItemRegex();

    public static (SchematicNumber[] numbers, SchematicSymbol[] symbols) ParseItems(string input, int index)
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
}
