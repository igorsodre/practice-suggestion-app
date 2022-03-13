using MongoDB.Driver;
using SuggestionApp.Library.Extensions;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class SuggestionService
{
    private readonly IDataStore _store;
    private readonly ISuggestionRepository _suggestionRepository;
    private readonly IUserRepository _userRepository;

    public SuggestionService(
        IDataStore store,
        IUserRepository userRepository,
        ISuggestionRepository suggestionRepository
    )
    {
        _store = store;
        _userRepository = userRepository;
        _suggestionRepository = suggestionRepository;
    }

    public async Task UpvoteSuggestion(string suggestionId, string userId)
    {
        await _store.ExecuteScoped(
            async scopedStore => {
                var (suggestion, isUpvote) = await HandleSuggestionUpvote(suggestionId, userId, scopedStore);

                await HandleUserUpvote(suggestionId, userId, isUpvote, suggestion, scopedStore);
            }
        );
        _suggestionRepository.InvalidateCache();
    }

    private async Task<(SuggestionModel suggestion, bool isUpvote)> HandleSuggestionUpvote(
        string suggestionId,
        string userId,
        IDataStore scopedStore
    )
    {
        var suggestion = (await scopedStore.Suggestions.FindAsync(s => s.Id == suggestionId)).First();

        var isUpvote = suggestion.UserVotes.Add(userId);

        if (!isUpvote)
        {
            suggestion.UserVotes.Remove(userId);
        }

        await scopedStore.Suggestions.ReplaceOneAsync(s => s.Id == suggestionId, suggestion);
        return (suggestion, isUpvote);
    }

    private async Task HandleUserUpvote(
        string suggestionId,
        string userId,
        bool isUpvote,
        SuggestionModel suggestion,
        IDataStore scopedStore
    )
    {
        var user = await _userRepository.GetUser(userId);

        if (isUpvote)
        {
            user.VotedOnSuggestions.Add(suggestion.ToBasicModel());
        }
        else
        {
            var suggestionsToRemove = user.VotedOnSuggestions.First(s => s.Id == suggestionId);
            user.VotedOnSuggestions.Remove(suggestionsToRemove);
        }

        await scopedStore.Users.ReplaceOneAsync(u => u.Id == userId, user);
    }
}
