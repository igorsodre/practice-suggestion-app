namespace SuggestionApp.UI.Interfaces;

internal interface IServiceInstaller
{
    IServiceCollection InstallServices(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment
    );
}
