namespace AdventOfCode.Solutions.Models;
public static class Directions
{
    public static readonly Point2D Up = (0, -1);
    public static readonly Point2D Down = (0, 1);
    public static readonly Point2D Left = (-1, 0);
    public static readonly Point2D Right = (1, 0);

    public static readonly Point2D North = (0, -1);
    public static readonly Point2D South = (0, 1);
    public static readonly Point2D West = (-1, 0);
    public static readonly Point2D East = (1, 0);

    public static readonly Point2D NorthEast = North + East;
    public static readonly Point2D NorthWest = North + West;
    public static readonly Point2D SouthEast = South + East;
    public static readonly Point2D SouthWest = South + West;
}
