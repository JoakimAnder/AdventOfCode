using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Solutions.Helpers;
public static class DependencyInjection
{
    public static IServiceCollection AddSolutions(this IServiceCollection services)
    {
        services.AddPuzzlesAndSolutions();
        return services;
    }
}
