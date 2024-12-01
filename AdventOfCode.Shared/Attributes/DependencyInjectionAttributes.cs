using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AdventOfCode.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public abstract class DependencyInjectionAttribute(Type? targetType, object? key) : Attribute
{
    public Type? TargetType { get; } = targetType;
    public object? Key { get; } = key;
}

public class SingletonAttribute(Type? targetType = null, object? key = null) : DependencyInjectionAttribute(targetType, key)
{
}

public class ScopedAttribute(Type? targetType = null, object? key = null) : DependencyInjectionAttribute(targetType, key)
{
}

public class TransientAttribute(Type? targetType = null, object? key = null) : DependencyInjectionAttribute(targetType, key)
{
}

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddAttributedDependencies(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Select(t => (Type: t, DIAttributes: t.GetCustomAttributes<DependencyInjectionAttribute>().ToArray()))
            .Where(t => t.DIAttributes.Length > 0);

        foreach (var (type, attributes) in types)
        {
            foreach (var attr in attributes)
            {
                var targetType = attr.TargetType ?? type;
                var lifeTime = ServiceLifetime.Singleton;
                if (attr is ScopedAttribute)
                    lifeTime = ServiceLifetime.Scoped;
                if (attr is TransientAttribute)
                    lifeTime = ServiceLifetime.Transient;

                var service = new ServiceDescriptor(targetType, attr.Key, type, lifeTime);
                services.Add(service);
            }
        }
        return services;
    }
}
