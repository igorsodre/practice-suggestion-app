using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using SuggestionApp.Library.Extensions;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class SuggestionRepository : ISuggestionRepository
{
    private const string SuggestionCacheKey = "4MlBL0zY8TN6l4eGO013clSoJGhaej73ROh";
    private readonly IMemoryCache _cache;
    private readonly IDataStore _store;
    private readonly IUserRepository _userRepository;
    private readonly IMongoCollection<SuggestionModel> _suggestionCollection;

    public SuggestionRepository(IDataStore store, IUserRepository userRepository, IMemoryCache cache)
    {
        _suggestionCollection = store.Suggestions;
        _store = store;
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task<IList<SuggestionModel>> GetSuggestions()
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

    public async Task<IList<SuggestionModel>> GetApprovedSuggestions()
    {
        var suggestions = await GetSuggestions();
        return suggestions.Where(s => s.ApprovedForRelease).ToList();
    }

    public async Task<SuggestionModel?> GetSuggestion(string id)
    {
        return await (await _suggestionCollection.FindAsync(s => s.Id == id)).FirstOrDefaultAsync();
    }

    public async Task<IList<SuggestionModel>> GetUserSuggestions(string userId)
    {
        var output = _cache.Get<List<SuggestionModel>>(userId);
        if (output is not null)
        {
            return output;
        }

        var query = await _suggestionCollection.FindAsync(s => s.Author.Id == userId);
        output = await query.ToListAsync();
        _cache.Set(userId, output, TimeSpan.FromMinutes(1));

        return output;
    }

    public async Task<IList<SuggestionModel>> GetSuggestionsWaitingForApproval()
    {
        var suggestions = await GetSuggestions();
        return suggestions.Where(s => !s.ApprovedForRelease && !s.Rejected).ToList();
    }

    public async Task CreateSuggestion(SuggestionModel suggestion)
    {
        await _store.ExecuteScoped(
            async scopedStore => {
                await scopedStore.Suggestions.InsertOneAsync(suggestion);
                var user = await _userRepository.GetUser(suggestion.Author.Id);
                user.AuthoredSuggestions.Add(suggestion.ToBasicModel());
                await scopedStore.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
            }
        );
    }

    public async Task UpdateSuggestion(SuggestionModel suggestion)
    {
        await _suggestionCollection.ReplaceOneAsync(s => s.Id == suggestion.Id, suggestion);
        InvalidateCache();
    }

    public void InvalidateCache()
    {
        _cache.Remove(SuggestionCacheKey);
    }
}
