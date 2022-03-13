using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.Interfaces;

internal interface ICategoryRepository
{
    Task<IEnumerable<CategoryModel>> GetCategories();

    Task CreateCategory(CategoryModel category);
}