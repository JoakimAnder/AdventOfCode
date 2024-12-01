
namespace AdventOfCode.Solutions.Puzzles.Year2024.Day02;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var sum = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;

            var calibrationValue = ParseCalibrationValue(line);
            sum += calibrationValue;
        }

        return ValueTask.FromResult<object>(sum);
    }

    private static int ParseCalibrationValue(ReadOnlySpan<char> line)
    {
        char? firstNum = null;
        char? lastNum = null;

        for (int i = 0; i < line.Length; i++)
        {
            var fromStart = line[i];
            var fromEnd = line[^(i + 1)];

            if (firstNum is null && char.IsNumber(fromStart))
                firstNum = fromStart;

            if (lastNum is null && char.IsNumber(fromEnd))
                lastNum = fromEnd;

            if (firstNum is not null && lastNum is not null)
                break;
        }

        var result = int.TryParse($"{firstNum}{lastNum}", out var r) ? r : 0;
        return result;
    }
}
