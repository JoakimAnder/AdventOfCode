using Infra.Models;

namespace Models;

public readonly record struct Image(IEnumerable<Galaxy> Galaxies)
{
    public readonly Image Expand(int expansion)
    {
        var populatedXSpaces = Galaxies
            .Select(g => g.Location.X)
            .Distinct()
            .Order()
            .ToArray();
        var populatedYSpaces = Galaxies
            .Select(g => g.Location.Y)
            .Distinct()
            .Order()
            .ToArray();

        var unpopulatedXSpaces = FindSpacesBetween(populatedXSpaces);
        var unpopulatedYSpaces = FindSpacesBetween(populatedYSpaces);

        var newGalaxies = Galaxies
            .Select(g =>
            {
                var xExpantion = unpopulatedXSpaces.Where(x => x.space < g.Location.X).Sum(x => x.size) * (expansion - 1);
                var yExpantion = unpopulatedYSpaces.Where(y => y.space < g.Location.Y).Sum(y => y.size) * (expansion - 1);
                return new Galaxy(g.Location + (xExpantion, yExpantion));
            })
            .ToArray();

        return new(newGalaxies);
    }

    private static (int space, int size)[] FindSpacesBetween(int[] spaces)
    {
        var min = spaces.Min();
        var max = spaces.Max();
        int? size = null;
        int? space = null;
        var emptySpaces = new List<(int space, int size)>();

        for (var i = min; i <= max; i++)
        {
            if (spaces.Contains(i))
            {
#pragma warning disable S2589 // Boolean expressions should not be gratuitous
                if (size.HasValue && space.HasValue)
#pragma warning restore S2589 // Boolean expressions should not be gratuitous
                {
                    emptySpaces.Add((space.Value, size.Value));
                }

                size = null;
                space = null;
                continue;
            }

            space ??= i;
            size ??= 0;
            size++;
        }

        return emptySpaces.ToArray();
    }
}

public readonly record struct Galaxy(Point2D Location)
{
    public readonly int DistanceTo(Galaxy other)
    {
        var difference = (other.Location - Location).Absolute();
        var distance = difference.X + difference.Y;
        return distance;
    }
}