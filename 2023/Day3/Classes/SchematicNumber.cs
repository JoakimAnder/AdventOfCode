using Infra.Classes;

namespace Day3.Classes;

public readonly record struct SchematicNumber(int Value, IEnumerable<Point2D> Placement)
{
}
