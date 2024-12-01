namespace AdventOfCode.Solutions.Puzzles.Year2024.Day01;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var totalDistance = 0;


        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;


            var calibrationValue = ParseCalibrationValue(line);
            sum += calibrationValue;
        }

        return ValueTask.FromResult<object>(totalDistance);
    }

    private Pair ParseLine(ReadOnlySpan<char> line)
    {
        line.Split
    }

    protected readonly record struct Pair(int Left, int Right);

}
