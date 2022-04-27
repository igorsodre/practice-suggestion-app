using SuggestionApp.Library.Extensions;
using SuggestionApp.Library.Models;

namespace SuggestionApp.UI.Models.Extensions;

internal static class SuggestionExtensions
{
    internal static SuggestionModel ToModel(
        this CreateSuggestionModel suggestion,
        UserModel userModel,
        CategoryModel category
    )
    {
        return new SuggestionModel
        {
            Title = suggestion.Title,
            Description = suggestion.Description,
            Author = userModel.ToBasicModel(),
            Category = category
        };
    }
}
