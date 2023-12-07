using Infra.Helpers;
using Models;
using System.Text.RegularExpressions;

namespace Puzzle;

public static partial class InputParser
{
    public static Map? ParseMap(string[] lines)
    {
        if (lines.Length == 0) return null;
        var (from, to) = ParseMapHeader(lines[0]);
        var ranges = lines.Skip(1)
            .Select(ParseMapRange)
            .ToArray();

        return new Map(from, to, ranges);
    }

    [GeneratedRegex(@"\w+(?=-to-)")]
    private static partial Regex MapFromRegex();
    [GeneratedRegex(@"(?<=-to-)\w+")]
    private static partial Regex MapToRegex();
    public static (string from, string to) ParseMapHeader(string line)
    {
        var from = MapFromRegex().Match(line).Value;
        var to = MapToRegex().Match(line).Value;
        return (from, to);
    }

    public static MapRange ParseMapRange(string line)
    {
        var r = Helper.NumberRegex();
        var matches = r.Matches(line)
            .Select(m => Helper.ParseLong(m.ValueSpan))
            .ToArray();

        var (destinationStart, sourceStart, length) = (matches[0], matches[1], matches[2]);
        return new MapRange(destinationStart, sourceStart, length);
    }
    public static (long[] seeds, IList<Map> maps) Parse(IEnumerable<string> lines)
    {
        var maps = new List<Map>();
        long[]? seeds = null;

        var mapLines = new List<string>();

        foreach (var line in lines)
        {
            if (seeds is null)
            {
                seeds = ParseSeeds(line);
                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                var map = ParseMap(mapLines.ToArray());
                if (map.HasValue)
                    maps.Add(map.Value);

#pragma warning disable S2583 // Conditionally executed code should be reachable // It is reachable...
                if (mapLines.Count > 0)
                    mapLines.Clear();
#pragma warning restore S2583 // Conditionally executed code should be reachable
                continue;
            }

            mapLines.Add(line);
        }

        var finalMap = ParseMap(mapLines.ToArray());
        if (finalMap.HasValue)
            maps.Add(finalMap.Value);
        return (seeds!, maps);
    }

    public static long[] ParseSeeds(string line)
    {
        var r = Helper.NumberRegex();
        var result = r.Matches(line)
            .Select(m => Helper.ParseLong(m.ValueSpan))
            .ToArray();
        return result;
    }

    public static (MapRange[] seeds, IList<Map> map) Parse2(IEnumerable<string> lines)
    {
        var maps = new List<Map>();
        MapRange[]? seeds = null;

        var mapLines = new List<string>();

        foreach (var line in lines)
        {
            if (seeds is null)
            {
                seeds = ParseSeedRanges(line);
                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                var map = ParseMap(mapLines.ToArray());
                if (map.HasValue)
                    maps.Add(map.Value);

#pragma warning disable S2583 // Conditionally executed code should be reachable // It is reachable...
                if (mapLines.Count > 0)
                    mapLines.Clear();
#pragma warning restore S2583 // Conditionally executed code should be reachable
                continue;
            }

            mapLines.Add(line);
        }

        var finalMap = ParseMap(mapLines.ToArray());
        if (finalMap.HasValue)
            maps.Add(finalMap.Value);

        return (seeds!, maps);
    }

    public static MapRange[] ParseSeedRanges(string line)
    {
        var r = Helper.NumberRegex();
        var result = r.Matches(line)
            .Select(m => Helper.ParseLong(m.ValueSpan))
            .ToArray();

        var ranges = new List<MapRange>();
        for (int i = 0; i < result.Length; i += 2)
        {
            var from = result[i];
            var length = result[i + 1];
            ranges.Add(new MapRange(from, from, length));
        }

        return ranges.ToArray();
    }
}
