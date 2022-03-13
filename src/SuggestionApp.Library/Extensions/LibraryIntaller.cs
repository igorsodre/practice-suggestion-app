using Microsoft.Extensions.DependencyInjection;
using SuggestionApp.Library.DataAccess;
using SuggestionApp.Library.Interfaces;

namespace SuggestionApp.Library.Extensions;

public static class LibraryIntaller
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, DataStoreSettings settings)
    {
        services.AddSingleton<IDataStore>(new DataStore(settings));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISuggestionRepository, SuggestionRepository>();
        return services;
    }
}
