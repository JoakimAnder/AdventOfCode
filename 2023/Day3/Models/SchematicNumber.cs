using Extentions;
using Infra.Models;

namespace Models;

public readonly record struct SchematicNumber(int Value, IEnumerable<Point2D> Placement)
{
    public bool IsMachinePart(Point2D[] symbols) =>
        Placement.Any(p => p.IsNextToAny(symbols));
}
