using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SuggestionApp.Library.Models;

public class StatusModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Decription { get; set; } = string.Empty;
}
