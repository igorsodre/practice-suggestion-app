using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface IStatusRepository
{
    Task<IEnumerable<StatusModel>> GetStatuses();

    Task CreateStatus(StatusModel model);
}
