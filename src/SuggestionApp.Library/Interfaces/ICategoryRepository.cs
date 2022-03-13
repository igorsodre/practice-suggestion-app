using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryModel>> GetCategories();

    Task CreateCategory(CategoryModel category);
}