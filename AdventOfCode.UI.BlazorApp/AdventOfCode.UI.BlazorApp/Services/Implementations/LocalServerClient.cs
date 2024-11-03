using AdventOfCode.Shared.Attributes;
using AdventOfCode.UI.BlazorApp.Client.Clients;
using AdventOfCode.UI.Shared.Services;

namespace AdventOfCode.UI.BlazorApp.Services.Implementations;

[Singleton(typeof(IServerClient))]
public class LocalServerClient(IServiceProvider serviceProvider) : IServerClient
{
    public Task<Puzzle?> GetPuzzle(int year, int day, CancellationToken ct)
        => Endpoints.Puzzles.GetPuzzle(year, day, serviceProvider, ct);
    public Task<SolutionResult?> TrySolution(int year, int day, int stars, RunSolutionRequest body, CancellationToken ct)
        => Endpoints.Puzzles.RunPuzzle(year, day, stars, body, serviceProvider.GetRequiredService<ISolutionRunnerService>(), serviceProvider, ct);
}
