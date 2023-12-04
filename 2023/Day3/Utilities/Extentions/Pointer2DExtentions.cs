using Infra.Models;

namespace Extentions;

public static class Pointer2DExtentions
{
    public static bool IsNextToAny(this Point2D point, IEnumerable<Point2D> numberPlacement)
    {
        var minX = point.X - 1;
        var maxX = point.X + 1;
        return numberPlacement.Any(s => s.X >= minX && s.X <= maxX);
    }
}
