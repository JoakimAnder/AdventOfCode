
namespace AdventOfCode.Solutions.Puzzles.Year2021.Day01;

public class TwoStarSolution : ISolution
{
    private static int WINDOW_SIZE = 3;
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        int? lastMeasurement = null;
        var increases = 0;
        var measurements = new List<int?>();

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;

            var depth = int.TryParse(line, out var x) ? (int?)x : null;
            measurements.Add(depth);
            if (measurements.Count < WINDOW_SIZE)
                continue;

            var currentMeasurement = measurements.Sum();
            measurements.RemoveAt(0);

            if (lastMeasurement is null)
            {
                lastMeasurement = currentMeasurement;
                continue;
            }

            if (currentMeasurement > lastMeasurement)
                increases++;

            lastMeasurement = currentMeasurement;
        }

        return ValueTask.FromResult<object>(increases);
    }


}
