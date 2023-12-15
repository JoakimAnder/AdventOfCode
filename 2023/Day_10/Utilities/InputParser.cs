using Infra.Interfaces;
using Infra.Models;
using Models;
using System.Text.RegularExpressions;

namespace Puzzle;

public static partial class InputParser
{
    [GeneratedRegex(@"#")]
    public static partial Regex GalaxyRegex();

    public static Image Parse(IInputReader reader)
    {
        var galaxies = reader.ParseLines(ParseLine)
            .SelectMany(l => l)
            .Select(p => new Galaxy(p))
            .ToArray();

        return new Image(galaxies);
    }
    private static Point2D[] ParseLine(string line, int y)
    {
        var galaxies = GalaxyRegex()
            .Matches(line)
            .Select(m => new Point2D(m.Index, y))
            .ToArray();

        return galaxies;
    }
}
