using SuggestionApp.Library;
using SuggestionApp.Library.Extensions;
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

        var storeSettings = new DataStoreSettings();
        configuration.GetSection("DataStoreSettings").Bind(storeSettings);
        services.AddDataStore(storeSettings);

        return services;
    }
}
