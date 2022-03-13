namespace SuggestionApp.Library.Interfaces;

internal interface ISuggestionService
{
    Task UpvoteSuggestion(string suggestionId, string userId);
}
