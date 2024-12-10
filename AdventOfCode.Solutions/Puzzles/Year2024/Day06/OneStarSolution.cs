using AdventOfCode.Solutions.Models;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day06;

public partial class OneStarSolution : ISolution
{
    private static readonly ImmutableArray<Point2D> Directions = [(0, -1), (1, 0), (0, 1), (-1, 0)];
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var (map, currentPosition, currentDirection) = ParseInput(input);

        HashSet<Point2D> visitedPoints = [];
        while (true)
        {
            if (map.IsOutOfBounds(currentPosition) || ct.IsCancellationRequested)
                break;

            var nextPosition = currentPosition + currentDirection;
            if (map.IsObstructed(nextPosition))
            {
                var currentIndex = Directions.IndexOf(currentDirection);
                var nextIndex = currentIndex + 1;
                if (nextIndex >= Directions.Length)
                    nextIndex = 0;
                currentDirection = Directions[nextIndex];
                continue;
            }

            visitedPoints.Add(currentPosition);
            currentPosition = nextPosition;
        }

        var count = visitedPoints.Count;
        return ValueTask.FromResult<object>(count);
    }

    private (Map map, Point2D startPosition, Point2D startDirection) ParseInput(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf(Environment.NewLine) + Environment.NewLine.Length;
        var height = input.Length / width;
        var startIndex = input.IndexOf('^');
        var startX = startIndex % width;
        var startY = startIndex / width;
        var startPosition = (startX, startY);
        var startDirection = (0, -1);

        var regex = ObstructionRegex();
        List<Point2D> obstructions = [];
        foreach (var match in regex.EnumerateMatches(input))
        {
            var x = match.Index % width;
            var y = match.Index / width;
            obstructions.Add((x, y));
        }

        var mapWidth = width - Environment.NewLine.Length;
        return (new Map(mapWidth, height, obstructions), startPosition, startDirection);
    }

    [GeneratedRegex("#")]
    private static partial Regex ObstructionRegex();

    private readonly record struct Map(int Width, int Height, IReadOnlyCollection<Point2D> Obstructions)
    {
        public readonly bool IsOutOfBounds(Point2D point)
            => point.X < 0 || point.Y < 0 || point.X > Width || point.Y > Height;

        public readonly bool IsObstructed(Point2D point)
            => Obstructions.Contains(point);
    }
}
