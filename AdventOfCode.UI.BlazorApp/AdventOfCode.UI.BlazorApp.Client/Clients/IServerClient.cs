using Refit;

namespace AdventOfCode.UI.BlazorApp.Client.Clients;

[Headers("Content-Type: application/json")]
public interface IServerClient
{
    [Get("/api/puzzles/{year}/day/{day}")]
    Task<Puzzle?> GetPuzzle(int year, int day, CancellationToken ct);

    [Post("/api/puzzles/{year}/day/{day}/{stars}")]
    Task<SolutionResult?> TrySolution(int year, int day, int stars, [Body] RunSolutionRequest body, CancellationToken ct);
}
