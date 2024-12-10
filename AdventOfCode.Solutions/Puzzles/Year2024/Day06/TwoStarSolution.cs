using AdventOfCode.Solutions.Models;
using static AdventOfCode.Solutions.Puzzles.Year2024.Day06.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day06;

public class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var (map, startPosition, startDirection) = ParseInput(input);
        var path = GetVisitedPoints(map, startPosition, startDirection, ct);

        List<Point2D> infiniteObstructions = [];
        foreach (var point in path.Where(p => p != startPosition))
        {
            if (ct.IsCancellationRequested)
                return ValueTask.FromResult<object>(0);

            var newMap = map with { Obstructions = [.. map.Obstructions, point] };
            var newPath = GetVisitedPoints(newMap, startPosition, startDirection, ct);

            if (newPath.Count == 0)
                infiniteObstructions.Add(point);
        }

        return ValueTask.FromResult<object>(infiniteObstructions.Count);
    }

    private static IReadOnlyCollection<Point2D> GetVisitedPoints(Map map, Point2D currentPosition, Point2D currentDirection, CancellationToken ct)
    {
        HashSet<Point2D> visitedPoints = [];
        HashSet<(Point2D position, Point2D direction)> pastStates = [];
        while (true)
        {
            if (pastStates.Contains((currentPosition, currentDirection)))
                return [];
            pastStates.Add((currentPosition, currentDirection));

            if (map.IsOutOfBounds(currentPosition) || ct.IsCancellationRequested)
                break;

            var nextPosition = currentPosition + currentDirection;
            if (map.IsObstructed(nextPosition))
            {
                var currentIndex = WalkDirections.IndexOf(currentDirection);
                var nextIndex = currentIndex + 1;
                if (nextIndex >= WalkDirections.Length)
                    nextIndex = 0;
                currentDirection = WalkDirections[nextIndex];
                continue;
            }

            visitedPoints.Add(currentPosition);
            currentPosition = nextPosition;
        }

        return visitedPoints;
    }


}
