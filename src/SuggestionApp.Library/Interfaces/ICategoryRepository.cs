using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface ICategoryRepository
{
    Task<IList<CategoryModel>> GetCategories();

    Task CreateCategory(CategoryModel category);
}
