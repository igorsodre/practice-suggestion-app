using SuggestionApp.Library.Models;
using SuggestionApp.Library.Models.Simplified;

namespace SuggestionApp.Library.Extensions;

public static class SuggestionModelExtenstions
{
    public static BasicSuggestionModel ToBasicModel(this SuggestionModel model)
    {
        return new BasicSuggestionModel
        {
            Id = model.Id,
            Title = model.Title
        };
    }
}
