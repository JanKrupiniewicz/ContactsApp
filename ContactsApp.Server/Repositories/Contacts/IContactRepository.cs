namespace ContactsApp.Server.Repositories.Contacts
{
    public interface IContactRepository
    {
        Task<List<Models.Contacts>> GetAllContactsAsync();
        Task<Models.Contacts> GetContactByIdAsync(int id);
        Task<Models.Contacts> AddContactAsync(Models.Contacts contact);
        Task<Models.Contacts> UpdateContactAsync(Models.Contacts contact);
        Task<bool> DeleteContactAsync(int id);
    }
}
