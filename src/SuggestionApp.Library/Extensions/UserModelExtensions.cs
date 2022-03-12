using SuggestionApp.Library.Models;
using SuggestionApp.Library.Models.Simplified;

namespace SuggestionApp.Library.Extensions;

public static class UserModelExtensions
{
    public static BasicUserModel ToBasicModel(this UserModel model)
    {
        return new BasicUserModel
        {
            Id = model.Id,
            DisplayName = model.DisplayName
        };
    }
}
