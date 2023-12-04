using Day2.Classes;
using Infra.Helpers;
using Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Puzzle;

public static partial class InputParser
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex GenerateGameIdRegex();

    private static readonly Regex GameIdRegex = GenerateGameIdRegex();

    public static Game ParseGame(string line)
    {
        //line = Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        var idMatch = GameIdRegex.Match(line);
        var id = int.Parse(idMatch.ValueSpan, NumberFormatInfo.InvariantInfo);
        var strDraws = line.Split(':')[1];
        var draws = strDraws.Split(';')
            .Select(ParseDraw);

        return new Game(id, draws);
    }
    private static IDictionary<CubeColor, int> ParseDraw(string strDraw)
    {
        //strDraw = 3 blue, 4 red
        return strDraw.Split(',')
            .Select(cubeDraw => cubeDraw.Trim().Split(' '))
            .Select(num_Color => (Enum.Parse<CubeColor>(num_Color[1], true), Helper.ParseInt(num_Color[0])))
            .ToDictionary();
    }
}
