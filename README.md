# Vertical_Slice_Architecture

// todo
using BAL.Serviecx;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBAL(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjectionExtensions).Assembly;

        services.Scan(scan =>
        scan.FromAssemblies(assembly)
        .AddClasses(classes => classes.InNamespaces("BAL"))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        return services;
    }
}