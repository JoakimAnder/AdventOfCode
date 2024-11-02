using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day02;

public partial class OneStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var constraints = new Dictionary<CubeColor, int> {
            { CubeColor.Red, 12 },
            { CubeColor.Green, 13 },
            { CubeColor.Blue, 14 },
        };

        var sum = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            var game = ParseGame(line.ToString());
            var points = CalculatePoints(game, constraints);
            sum += points;
        }

        return ValueTask.FromResult<object>(sum);
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex GameIdRegex();

    public static Game ParseGame(string line)
    {
        //line = Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        var idMatch = GameIdRegex().Match(line);
        var id = int.Parse(idMatch.ValueSpan, NumberFormatInfo.InvariantInfo);
        var strDraws = line.Split(':')[1];
        var draws = strDraws.Split(';')
            .Select(ParseDraw);

        return new Game(id, draws);
    }
    private static IReadOnlyDictionary<CubeColor, int> ParseDraw(string strDraw)
    {
        //strDraw = 3 blue, 4 red
        return strDraw.Split(',')
            .Select(cubeDraw => cubeDraw.Trim().Split(' '))
            .Select(num_Color => (Enum.Parse<CubeColor>(num_Color[1], true), int.Parse(num_Color[0])))
            .ToDictionary();
    }

    public int CalculatePoints(Game game, IDictionary<CubeColor, int> constraints) => IsPossible(game, constraints) ? game.Id : 0;
    private bool IsPossible(Game game, IDictionary<CubeColor, int> constraints)
    {
        foreach (var draw in game.Draws)
        {
            foreach (var kv in draw)
            {
                constraints.TryGetValue(kv.Key, out var maxCubes);
                if (maxCubes < kv.Value)
                    return false;
            }
        }
        return true;
    }

    public enum CubeColor
    {
        Red,
        Green,
        Blue,
    }

    public class Game(int id, IEnumerable<IReadOnlyDictionary<CubeColor, int>> draws)
    {
        public int Id { get; } = id;
        public IReadOnlyList<IReadOnlyDictionary<CubeColor, int>> Draws { get; } = draws.ToList();
    }

}
