namespace Infra.Models;

public readonly record struct Point2D(int X, int Y)
{
    public readonly Point2D Normalized()
    {
        var xx = X / Absolute().X;
        var yy = Y / Absolute().Y;
        return new Point2D(xx, yy);
    }

    public readonly Point2D Absolute()
    {
        var xx = Math.Abs(X);
        var yy = Math.Abs(Y);
        return new Point2D(xx, yy);
    }

    public static implicit operator Point2D((int x, int y) tuple) => ToPoint2D(tuple);
    public static Point2D ToPoint2D((int x, int y) tuple) => new(tuple.x, tuple.y);



    public static Point2D operator +(Point2D left, Point2D right) => left.Add(right);
    public Point2D Add(Point2D other)
    {
        var xx = X + other.X;
        var yy = Y + other.Y;
        return new Point2D(xx, yy);
    }

    public static Point2D operator -(Point2D left, Point2D right) => left.Subtract(right);
    public Point2D Subtract(Point2D other)
    {
        var xx = X - other.X;
        var yy = Y - other.Y;
        return new Point2D(xx, yy);
    }

    public static Point2D operator *(Point2D left, Point2D right) => left.Multiply(right);

    public Point2D Multiply(Point2D other)
    {
        var xx = X * other.X;
        var yy = Y * other.Y;
        return new Point2D(xx, yy);
    }

}

