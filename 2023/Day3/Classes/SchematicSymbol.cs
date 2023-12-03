using Infra.Classes;

namespace Day3.Classes;

public readonly struct SchematicSymbol(char value, Point2D placement) : ISchematicItem, IEquatable<SchematicSymbol>
{
    public char Value => value;
    public Point2D Placement => placement;



    public override bool Equals(object? obj) => obj is SchematicSymbol other && Equals(other);

    public bool Equals(SchematicSymbol other) => value == other.Value && Placement == other.Placement;

    public override int GetHashCode() => (value, placement).GetHashCode();

    public static bool operator ==(SchematicSymbol left, SchematicSymbol right) => left.Equals(right);

    public static bool operator !=(SchematicSymbol left, SchematicSymbol right) => !(left == right);
}
