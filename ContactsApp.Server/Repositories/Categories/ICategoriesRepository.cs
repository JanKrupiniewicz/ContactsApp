namespace ContactsApp.Server.Repositories.Categories
{
    public interface ICategoriesRepository
    {
        Task<List<Models.Categories>> GetAllCategoriesAsync();
    }
}
