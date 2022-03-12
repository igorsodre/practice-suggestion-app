using SuggestionApp.UI.Interfaces;

namespace SuggestionApp.UI.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment
    )
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(type => typeof(IServiceInstaller).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var installer in installers)
        {
            installer.InstallServices(services, configuration, environment);
        }

        return services;
    }
}
