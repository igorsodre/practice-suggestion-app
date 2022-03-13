using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _userCollection;

    public UserRepository(IDataStore store)
    {
        _userCollection = store.Users;
    }

    public async Task<UserModel> GetUser(string id)
    {
        var result = await _userCollection.FindAsync(u => u.Id == id);
        return result.FirstOrDefault();
    }

    public async Task<UserModel> GetUserFromAuthentication(string objectId)
    {
        var result = await _userCollection.FindAsync(u => u.ObjectIdentifier == objectId);
        return result.FirstOrDefault();
    }

    public async Task CreateUser(UserModel user)
    {
        await _userCollection.InsertOneAsync(user);
    }

    public async Task UpdateUser(UserModel user)
    {
        var filter = Builders<UserModel>.Filter.Eq(u => u.Id, user.Id);

        await _userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
    }
}
