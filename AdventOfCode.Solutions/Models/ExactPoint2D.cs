namespace AdventOfCode.Solutions.Models;

public readonly record struct ExactPoint2D(double X, double Y)
{
    public readonly ExactPoint2D Normalized()
    {
        var xx = X / Absolute().X;
        var yy = Y / Absolute().Y;
        return new ExactPoint2D(xx, yy);
    }

    public readonly ExactPoint2D Absolute()
    {
        var xx = Math.Abs(X);
        var yy = Math.Abs(Y);
        return new ExactPoint2D(xx, yy);
    }

    public static implicit operator ExactPoint2D((double x, double y) tuple) => ToExactPoint2D(tuple);
    public static ExactPoint2D ToExactPoint2D((double x, double y) tuple) => new(tuple.x, tuple.y);



    public static ExactPoint2D operator +(ExactPoint2D left, ExactPoint2D right) => left.Add(right);
    public ExactPoint2D Add(ExactPoint2D other)
    {
        var xx = X + other.X;
        var yy = Y + other.Y;
        return new ExactPoint2D(xx, yy);
    }

    public static ExactPoint2D operator -(ExactPoint2D left, ExactPoint2D right) => left.Subtract(right);
    public ExactPoint2D Subtract(ExactPoint2D other)
    {
        var xx = X - other.X;
        var yy = Y - other.Y;
        return new ExactPoint2D(xx, yy);
    }

    public static ExactPoint2D operator *(ExactPoint2D left, ExactPoint2D right) => left.Multiply(right);

    public ExactPoint2D Multiply(ExactPoint2D other)
    {
        var xx = X * other.X;
        var yy = Y * other.Y;
        return new ExactPoint2D(xx, yy);
    }

}

