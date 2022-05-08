using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface ISuggestionRepository
{
    Task<IList<SuggestionModel>> GetSuggestions();

    Task<IList<SuggestionModel>> GetApprovedSuggestions();

    Task<SuggestionModel?> GetSuggestion(string id);

    Task<IList<SuggestionModel>> GetUserSuggestions(string userId);

    Task<IList<SuggestionModel>> GetSuggestionsWaitingForApproval();

    Task CreateSuggestion(SuggestionModel suggestion);

    Task UpdateSuggestion(SuggestionModel suggestion);

    void InvalidateCache();
}
