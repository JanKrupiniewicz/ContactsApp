using ContactsApp.Server.Data;
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
    }
}
