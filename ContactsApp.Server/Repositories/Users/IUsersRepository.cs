namespace ContactsApp.Server.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<Models.Users?> GetByEmailAsync(string email);
        Task AddUserAsync(Models.Users user);
        Task SaveChangesAsync();
    }
}
