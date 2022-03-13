using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using SuggestionApp.Library.Interfaces;
using SuggestionApp.Library.Models;

namespace SuggestionApp.Library.DataAccess;

internal class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<CategoryModel> _categoryCollection;
    private readonly IMemoryCache _cache;
    private const string CategoryCacheKey = "FsswqCwwnd0esL5Qc8zMeoCk0ikoMv6B44o";

    public CategoryRepository(IDataStore store, IMemoryCache cache)
    {
        _cache = cache;
        _categoryCollection = store.Categories;
    }

    public async Task<IEnumerable<CategoryModel>> GetCategories()
    {
        var output = _cache.Get<IList<CategoryModel>>(CategoryCacheKey);
        if (output is not null)
        {
            return output;
        }

        var query = await _categoryCollection.FindAsync(_ => true);
        var result = await query.ToListAsync();

        _cache.Set(CategoryCacheKey, result, TimeSpan.FromHours(6));

        return result;
    }

    public async Task CreateCategory(CategoryModel category)
    {
        await _categoryCollection.InsertOneAsync(category);
    }
}
