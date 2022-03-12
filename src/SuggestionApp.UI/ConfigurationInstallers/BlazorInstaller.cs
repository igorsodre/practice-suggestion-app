using SuggestionApp.UI.Interfaces;

namespace SuggestionApp.UI.ConfigurationInstallers;

public class BlazorInstaller : IServiceInstaller
{
    public IServiceCollection InstallServices(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment
    )
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddMemoryCache();
        
        return services;
    }
}
