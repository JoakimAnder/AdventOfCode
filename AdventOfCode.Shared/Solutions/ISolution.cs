namespace AdventOfCode.Shared.Solutions;
public interface ISolution
{
    ValueTask<object> Solve(string input, CancellationToken ct);
}