namespace SuggestionApp.Library.Interfaces;

public interface ISuggestionService
{
    Task UpvoteSuggestion(string suggestionId, string userId);
}
