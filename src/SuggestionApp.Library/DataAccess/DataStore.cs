using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class DataStore : IDataStore
{
    public IMongoCollection<CategoryModel> Categories { get; init; }

    public IMongoCollection<StatusModel> Status { get; init; }

    public IMongoCollection<SuggestionModel> Suggestions { get; init; }

    public IMongoCollection<UserModel> Users { get; init; }

    public DataStore(DataStoreSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        Categories = database.GetCollection<CategoryModel>(nameof(Categories));
        Status = database.GetCollection<StatusModel>(nameof(Status));
        Suggestions = database.GetCollection<SuggestionModel>(nameof(Suggestions));
        Users = database.GetCollection<UserModel>(nameof(Users));
    }
}
