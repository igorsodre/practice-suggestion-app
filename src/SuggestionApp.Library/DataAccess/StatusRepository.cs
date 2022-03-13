using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class StatusRepository : IStatusRepository
{
    private const string StatusCacheKey = "e23alRTUsHjzJNT1I4IYudNkvpcdcdmgs1K";
    private readonly IMemoryCache _cache;
    private readonly IMongoCollection<StatusModel> _statusCollection;

    public StatusRepository(IDataStore store, IMemoryCache cache)
    {
        _cache = cache;
        _statusCollection = store.Status;
    }

    public async Task<IEnumerable<StatusModel>> GetStatuses()
    {
        var output = _cache.Get<IEnumerable<StatusModel>>(StatusCacheKey);
        if (output is not null)
        {
            return output;
        }

        var query = await _statusCollection.FindAsync(_ => true);
        var result = await query.ToListAsync();
        _cache.Set(StatusCacheKey, result, TimeSpan.FromHours(6));

        return result;
    }

    public async Task CreateStatus(StatusModel model)
    {
        await _statusCollection.InsertOneAsync(model);
    }
}
