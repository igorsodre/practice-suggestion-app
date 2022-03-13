using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

internal interface IUserRepository
{
    Task<UserModel> GetUser(string id);

    Task<UserModel> GetUserFromAuthentication(string objectId);

    Task CreateUser(UserModel user);

    Task UpdateUser(UserModel user);
}
