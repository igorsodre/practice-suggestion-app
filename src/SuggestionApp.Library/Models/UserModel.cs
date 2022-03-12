using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SuggestionApp.Library.Models.Simplified;

namespace SuggestionApp.Library.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string ObjectIdentifier { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IList<BasicSuggestionModel> AuthoredSuggestions { get; set; } = new List<BasicSuggestionModel>();

    public IList<BasicSuggestionModel> VotedOnSuggestions { get; set; } = new List<BasicSuggestionModel>();
}
