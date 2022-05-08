using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface IUserRepository
{
    Task<UserModel> GetUser(string id);

    Task<UserModel?> GetUserFromAuthentication(string objectId);

    Task CreateUser(UserModel user);

    Task UpdateUser(UserModel user);
}
