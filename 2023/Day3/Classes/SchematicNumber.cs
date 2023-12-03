using Infra.Classes;

namespace Day3.Classes;

public readonly struct SchematicNumber(int value, Point2D[] placement) : ISchematicItem, IEquatable<SchematicNumber>
{
    public int Value => value;
    public IEnumerable<Point2D> Placement => placement;



    public override bool Equals(object? obj) => obj is SchematicNumber other && Equals(other);

    public bool Equals(SchematicNumber other) => value == other.Value && Placement == other.Placement;

    public override int GetHashCode() => (value, placement).GetHashCode();

    public static bool operator ==(SchematicNumber left, SchematicNumber right) => left.Equals(right);

    public static bool operator !=(SchematicNumber left, SchematicNumber right) => !(left == right);

}
