using Infra.Helpers;
using Models;

namespace Puzzle;

public static class InputParser
{
    public static Race[] Parse(IEnumerable<string> lines)
    {
        var enumeratedLines = lines.ToList();
        var numberRegex = Helper.NumberRegex();
        var times = numberRegex.Matches(enumeratedLines[0]).Select((m, i) => (id: i, time: Helper.ParseLong(m.ValueSpan)));
        var distances = numberRegex.Matches(enumeratedLines[1]).Select((m, i) => (id: i, distance: Helper.ParseLong(m.ValueSpan)));

        var races = times
            .Join(distances, t => t.id, d => d.id, (t, d) => (t.time, d.distance, t.id))
            .Select(td => new Race(td.id, td.time, td.distance))
            .ToArray();

        return races;
    }
}
