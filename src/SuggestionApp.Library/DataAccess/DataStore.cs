using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class DataStore : IDataStore
{
    private readonly DataStoreSettings _settings;
    private readonly MongoClient _client;

    public IMongoCollection<CategoryModel> Categories { get; private set; } = null!;

    public IMongoCollection<StatusModel> Status { get; private set; } = null!;

    public IMongoCollection<SuggestionModel> Suggestions { get; private set; } = null!;

    public IMongoCollection<UserModel> Users { get; private set; } = null!;

    public DataStore(DataStoreSettings settings)
    {
        _settings = settings;
        _client = new MongoClient(_settings.ConnectionString);

        SetupCollections();
    }

    private void SetupCollections()
    {
        var database = _client.GetDatabase(_settings.DatabaseName);

        Categories = database.GetCollection<CategoryModel>(nameof(Categories));
        Status = database.GetCollection<StatusModel>(nameof(Status));
        Suggestions = database.GetCollection<SuggestionModel>(nameof(Suggestions));
        Users = database.GetCollection<UserModel>(nameof(Users));
    }

    private DataStore(MongoClient client, DataStoreSettings settings)
    {
        _settings = settings;
        _client = client;
        SetupCollections();
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
        catch(Exception ex)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}
