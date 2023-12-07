namespace Models;

public readonly record struct Map(string From, string To, IEnumerable<MapRange> Ranges)
{
    public readonly long MapValue(long from)
    {
        var range = Ranges.FirstOrDefault(r => r.ContainsSource(from));
        if (range == default)
            return from;
        return range.DestinationStart + range.IndexOf(from);
    }

    public readonly IEnumerable<MapRange> MapRange(MapRange from)
    {
        var (unmappable, mappable) = from.LeftJoin(Ranges);
        var mapped = mappable.Select(m => new MapRange(m.DestinationStart, m.DestinationStart, m.Length));
        return unmappable.Union(mapped).Distinct().ToArray();
    }



}


public readonly record struct MapRange(long DestinationStart, long SourceStart, long Length)
{
    public readonly long SourceEnd => SourceStart + Length;
    public readonly long DestinationEnd => DestinationStart + Length;

    public readonly bool ContainsSource(long value) => SourceStart <= value && value <= SourceEnd;

    public readonly (MapRange? left, MapRange? both, MapRange? right) LeftJoin(MapRange other)
    {

        if (other.ContainsSource(SourceStart) && other.ContainsSource(SourceEnd))
        {
            var startDestination = other.DestinationStart + (SourceStart - other.SourceStart);
            var both = new MapRange(startDestination, SourceStart, Length);
            return (null, both, null);
        }

        if (ContainsSource(other.SourceStart) && ContainsSource(other.SourceEnd))
        {
            MapRange? left = null;
            if (SourceStart != other.SourceStart)
                left = new MapRange(DestinationStart, SourceStart, other.SourceStart - SourceStart - 1);
            var both = other;
            MapRange? right = null;
            if (SourceEnd != other.SourceEnd)
                right = new MapRange(DestinationStart + (other.SourceEnd + 1 - SourceStart), other.SourceEnd + 1, SourceEnd - (other.SourceEnd + 1));
            return (left, both, right);
        }

        if (ContainsSource(other.SourceStart))
        {
            var left = new MapRange(DestinationStart, SourceStart, other.SourceStart - SourceStart - 1);
            var both = new MapRange(other.DestinationStart, other.SourceStart, SourceEnd - other.SourceStart);
            return (left, both, null);
        }

        if (ContainsSource(other.SourceEnd))
        {
            var both = new MapRange(other.DestinationStart + (SourceStart - other.SourceStart), SourceStart, other.SourceEnd - SourceStart);
            var right = new MapRange(DestinationStart + (other.SourceEnd + 1 - SourceStart), other.SourceEnd + 1, SourceEnd - (other.SourceEnd + 1));
            return (null, both, right);
        }

        return SourceStart < other.SourceStart ? (this, null, null) : (null, null, this);

    }

    public readonly (IList<MapRange> left, IList<MapRange> both) LeftJoin(IEnumerable<MapRange> others, bool stopRecursive = false)
    {
        var mappable = new List<MapRange>();
        var unmappable = new List<MapRange>();
        foreach (var r in others)
        {
            var (left, both, right) = LeftJoin(r);
            if (both.HasValue)
                mappable.Add(both.Value);
            if (left.HasValue)
                unmappable.Add(left.Value);
            if (right.HasValue)
                unmappable.Add(right.Value);
        }

        if (stopRecursive)
            return (unmappable, mappable);

        var trulyUnmappable = unmappable.SelectMany(um => um.LeftJoin(mappable, true).left)
            .ToList();
        return (trulyUnmappable, mappable);
    }
    public readonly long IndexOf(long value) => ContainsSource(value) ? value - SourceStart : -1;

}

