using ContactsApp.Server.Data;
using ContactsApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataContext _context;
        public CategoriesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Models.Categories>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.Subcategories).ToListAsync();
        }

        public async Task<Models.Categories?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Name == categoryName);
        }

        public async Task<Subcategories?> GetSubcategoryByNameAsync(string subcategoryName)
        {
            return await _context.Subcategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Name == subcategoryName);
        }

        public async Task<Subcategories> AddSubcategoryAsync(Subcategories subcategory)
        {
            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        public async Task<Subcategories?> GetSubcategoryByNameAndCategoryIdAsync(string name, int categoryId)
        {
            return await _context.Subcategories
                .FirstOrDefaultAsync(s => s.Name == name && s.CategoryId == categoryId);
        }

    }
}
