using Infra.Helpers;
using Models;

namespace Puzzle;

public static class InputParser
{
    public static History Parse(string line)
    {
        var readings = Helper.NumberRegex()
            .Matches(line)
            .Select(m => Helper.ParseInt(m.ValueSpan))
            .ToArray();

        return new History(readings);
    }
}
