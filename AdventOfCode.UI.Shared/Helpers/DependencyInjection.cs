using AdventOfCode.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.UI.Shared.Helpers;
public static class DependencyInjection
{
    public static IServiceCollection AddAdventOfCodeServices(this IServiceCollection services)
    {
        services.AddAttributedDependencies(typeof(DependencyInjection).Assembly);
        return services;
    }
}
