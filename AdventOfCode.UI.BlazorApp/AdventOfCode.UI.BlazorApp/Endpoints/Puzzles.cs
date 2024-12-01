using AdventOfCode.Shared.Solutions;
using AdventOfCode.UI.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.UI.BlazorApp.Endpoints;

public class Puzzles
{
    public static void AddEndpoints(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/api/puzzles/{year:int}/day/{day:int}", GetPuzzle);
        routeBuilder.MapPost("/api/puzzles/{year:int}/day/{day:int}/{stars:int}", RunPuzzle);
    }

    public static Task<Puzzle?> GetPuzzle(
        int year,
        int day,
        IServiceProvider services,
        CancellationToken ct)
    {
        var puzzle = services.GetKeyedService<IPuzzle>($"{year}.{day}");
        if (puzzle is null)
            return Task.FromResult<Puzzle?>(null);

        var name = puzzle.Name;
        var oneStarCode = puzzle.OneStarSolutionCode;
        var twoStarCode = puzzle.TwoStarSolutionCode;

        var result = new Puzzle(year, day, name, oneStarCode, twoStarCode);
        return Task.FromResult<Puzzle?>(result);
    }

    public static async Task<SolutionResult?> RunPuzzle(
        int year,
        int day,
        int stars,
        [FromBody] RunSolutionRequest body,
        ISolutionRunnerService runner,
        IServiceProvider services,
        CancellationToken ct)
    {
        var key = $"{year}.{day}.{stars}";
        var solution = services.GetKeyedService<ISolution>(key);
        if (solution is null)
            return null;

        var result = await runner.Run(solution, body.Input, ct);
        return result;
    }

}
