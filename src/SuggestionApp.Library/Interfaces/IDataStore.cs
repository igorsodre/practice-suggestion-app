using MongoDB.Driver;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface IDataStore
{
    IMongoCollection<CategoryModel> Categories { get; init; }

    IMongoCollection<StatusModel> Status { get; init; }

    IMongoCollection<SuggestionModel> Suggestions { get; init; }

    IMongoCollection<UserModel> Users { get; init; }
}
