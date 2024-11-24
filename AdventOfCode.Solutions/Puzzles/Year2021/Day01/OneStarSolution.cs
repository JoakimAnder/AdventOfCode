
namespace AdventOfCode.Solutions.Puzzles.Year2021.Day01;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        int? lastMeasurement = null;
        var increases = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;

            var currentMeasurement = int.TryParse(line, out int x) ? (int?)x : null;

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
