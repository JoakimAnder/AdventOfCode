
namespace AdventOfCode.Solutions.Puzzles.Year2021.Day02;

public class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var aim = 0;
        var depth = 0;
        var horizontalPosition = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            if (ct.IsCancellationRequested)
                break;

            var (direction, amount) = Parse(line.ToString());

            if (direction == "down")
            {
                aim += amount;
                continue;
            }
            if (direction == "up")
            {
                aim -= amount;
                continue;
            }

            horizontalPosition += amount;
            depth += aim * amount;
        }

        return ValueTask.FromResult<object>(new { Depth = depth, HorizontalPosirion = horizontalPosition, Multiplied = depth * horizontalPosition });
    }

    private static (string direction, int amount) Parse(string line)
    {
        var split = line.Split(' ');
        var direction = split[0];
        var amount = int.Parse(split[1]);
        return (direction, amount);
    }


}
