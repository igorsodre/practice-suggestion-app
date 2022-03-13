using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SuggestionApp.Library.Models.Simplified;

namespace SuggestionApp.Library.Models;

public class SuggestionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public CategoryModel? Category { get; set; }

    public BasicUserModel Author { get; set; } = new BasicUserModel();

    public ISet<string> UserVotes { get; set; } = new HashSet<string>();

    public StatusModel? Status { get; set; }

    public string OwnerNotes { get; set; } = string.Empty;

    public bool ApprovedForRelease { get; set; } = false;

    public bool Archived { get; set; } = false;

    public bool Rejected { get; set; } = false;
}
