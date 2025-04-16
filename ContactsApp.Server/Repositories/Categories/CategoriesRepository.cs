using ContactsApp.Server.Data;
using ContactsApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataContext _context;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="CategoriesRepository"/> z kontekstem bazy danych.
        /// </summary>
        /// <param name="context"></param>
        public CategoriesRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Pobiera wszystkie kategorie z bazy danych.
        /// </summary>
        /// <returns>Lista kategorii typu <see cref="Models.Categories"/>.</returns>
        public async Task<List<Models.Categories>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.Subcategories).ToListAsync();
        }
        /// <summary>
        /// Pobiera kategorię na podstawie jej nazwy.
        /// </summary>
        /// <param name="categoryName">Nazwa kategorii.</param>
        /// <returns>Obiekt kategorii typu <see cref="Models.Categories"/> lub null, jeśli nie znaleziono.</returns>
        public async Task<Models.Categories?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Name == categoryName);
        }
        /// <summary>
        /// Pobiera subkategorię na podstawie jej nazwy.
        /// </summary>
        /// <param name="subcategoryName">Nazwa subkategorii.</param>
        /// <returns>Obiekt subkategorii typu <see cref="Subcategories"/> lub null, jeśli nie znaleziono.</returns>
        public async Task<Subcategories?> GetSubcategoryByNameAsync(string subcategoryName)
        {
            return await _context.Subcategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Name == subcategoryName);
        }
        /// <summary>
        /// Dodaje nową subkategorię do bazy danych.
        /// </summary>
        /// <param name="subcategory">Obiekt subkategorii do dodania.</param>
        /// <returns>Zwraca dodaną subkategorię typu <see cref="Subcategories"/>.</returns>
        public async Task<Subcategories> AddSubcategoryAsync(Subcategories subcategory)
        {
            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }
        /// <summary>
        /// Pobiera subkategorię na podstawie jej nazwy oraz identyfikatora kategorii.
        /// </summary>
        /// <param name="name">Nazwa subkategorii.</param>
        /// <param name="categoryId">Identyfikator powiązanej kategorii.</param>
        /// <returns>Obiekt subkategorii typu <see cref="Subcategories"/> lub null, jeśli nie znaleziono.</returns>

        public async Task<Subcategories?> GetSubcategoryByNameAndCategoryIdAsync(string name, int categoryId)
        {
            return await _context.Subcategories
                .FirstOrDefaultAsync(s => s.Name == name && s.CategoryId == categoryId);
        }

    }
}
