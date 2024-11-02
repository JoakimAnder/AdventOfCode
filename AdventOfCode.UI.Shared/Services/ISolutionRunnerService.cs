using AdventOfCode.Shared.Solutions;

namespace AdventOfCode.UI.Shared.Services;
public interface ISolutionRunnerService
{
    ValueTask<SolutionResult> Run(ISolution solution, string input, CancellationToken ct);
}
