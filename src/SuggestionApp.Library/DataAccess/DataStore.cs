using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class DataStore : IDataStore
{
    private readonly DataStoreSettings _settings;
    private readonly MongoClient _client;

    public IMongoCollection<CategoryModel> Categories { get; private set; }

    public IMongoCollection<StatusModel> Status { get; private set; }

    public IMongoCollection<SuggestionModel> Suggestions { get; private set; }

    public IMongoCollection<UserModel> Users { get; private set; }

    public DataStore(DataStoreSettings settings)
    {
        _settings = settings;
        _client = new MongoClient(_settings.ConnectionString);

        var database = _client.GetDatabase(_settings.DatabaseName);

        Categories = database.GetCollection<CategoryModel>(nameof(Categories));
        Status = database.GetCollection<StatusModel>(nameof(Status));
        Suggestions = database.GetCollection<SuggestionModel>(nameof(Suggestions));
        Users = database.GetCollection<UserModel>(nameof(Users));
    }

    public async Task ExecuteScoped(Func<IDataStore, Task> procedure)
    {
        var store = new DataStore(_settings);
        using var session = await store._client.StartSessionAsync();
        try
        {
            session.StartTransaction();
            await procedure(store);
            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}
