using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

internal interface ISuggestionRepository
{
    Task<IEnumerable<SuggestionModel>> GetSuggestions();

    Task<IEnumerable<SuggestionModel>> GetApprovedSuggestions();

    Task<SuggestionModel?> GetSuggestion(string id);

    Task<IEnumerable<SuggestionModel>> GetSuggestionsWaitingForApproval();

    Task CreateSuggestion(SuggestionModel suggestion);
    Task UpdateSuggestion(SuggestionModel suggestion);

    void InvalidateCache();
}
