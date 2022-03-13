using MongoDB.Driver;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface IDataStore
{
    IMongoCollection<CategoryModel> Categories { get; }

    IMongoCollection<StatusModel> Status { get; }

    IMongoCollection<SuggestionModel> Suggestions { get; }

    IMongoCollection<UserModel> Users { get; }

    Task ExecuteScoped(Func<IDataStore, Task> procedure);
}
