using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Dtos.Contacts;
using ContactsApp.Server.Models;

namespace ContactsApp.Server.Services.Categories
{
    public interface ICategoriesService
    {
        Task<List<CategoriesDto>> GetAllCategoriesAsync();
    }
}
