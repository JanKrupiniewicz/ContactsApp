using ContactsApp.Server.Models;

namespace ContactsApp.Server.Repositories.Categories
{
    public interface ICategoriesRepository
    {
        Task<List<Models.Categories>> GetAllCategoriesAsync();
        Task<Models.Categories?> GetCategoryByNameAsync(string categoryName);
        Task<Models.Subcategories?> GetSubcategoryByNameAsync(string subcategoryName);
        Task<Subcategories> AddSubcategoryAsync(Subcategories subcategory);
        Task<Subcategories?> GetSubcategoryByNameAndCategoryIdAsync(string name, int categoryId);
    }
}
