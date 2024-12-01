
using AdventOfCode.Solutions.Models;

namespace AdventOfCode.Solutions.Puzzles.Year2021.Day02;

public class OneStarSolution : ISolution
{
    private static readonly IReadOnlyDictionary<string, Point2D> Directions = new Dictionary<string, Point2D>
    {
        ["forward"] = (1, 0),
        ["up"] = (0, -1),
        ["down"] = (0, 1),
    };

    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        Point2D position = (0, 0);
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;

            var point = Parse(line.ToString());
            position += point;
        }

        return ValueTask.FromResult<object>(new { Position = position, Multiplied = position.X * position.Y });
    }

    private static Point2D Parse(string line)
    {
        var splitLine = line.Split(' ');

        var direction = Directions[splitLine[0]];
        var amount = int.Parse(splitLine[1]);

        return direction * amount;
    }
}
