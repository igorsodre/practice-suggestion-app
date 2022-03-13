using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class SuggestionRepository : ISuggestionRepository
{
    private const string SuggestionCacheKey = "4MlBL0zY8TN6l4eGO013clSoJGhaej73ROh";
    private readonly IMemoryCache _cache;
    private readonly IUserRepository _userRepository;
    private readonly IMongoCollection<SuggestionModel> _suggestionCollection;

    public SuggestionRepository(IDataStore store, IUserRepository userRepository, IMemoryCache cache)
    {
        _suggestionCollection = store.Suggestions;
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<SuggestionModel>> GetSuggestions()
    {
        var output = _cache.Get<IList<SuggestionModel>>(SuggestionCacheKey);
        if (output is not null)
        {
            return output;
        }

        var query = await _suggestionCollection.FindAsync(s => !s.Archived);
        var result = await query.ToListAsync();

        _cache.Set(SuggestionCacheKey, result, TimeSpan.FromMinutes(1));

        return result;
    }

    public async Task<IEnumerable<SuggestionModel>> GetApprovedSuggestions()
    {
        var suggestions = await GetSuggestions();
        return suggestions.Where(s => s.ApprovedForRelease).ToList();
    }

    public async Task<SuggestionModel?> GetSuggestion(string id)
    {
        return await (await _suggestionCollection.FindAsync(s => s.Id == id)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SuggestionModel>> GetSuggestionsWaitingForApproval()
    {
        var suggestions = await GetSuggestions();
        return suggestions.Where(s => !s.ApprovedForRelease && !s.Rejected).ToList();
    }

    public async Task UpdateSuggestion(SuggestionModel suggestion)
    {
        await _suggestionCollection.ReplaceOneAsync(s => s.Id == suggestion.Id, suggestion);
        _cache.Remove(SuggestionCacheKey);
    }

    public void InvalidateCache()
    {
        _cache.Remove(SuggestionCacheKey);
    }
}
