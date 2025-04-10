namespace ContactsApp.Server.Services.Contacts
{
    public interface IContactService
    {
        Task<List<ContactsApp.Server.Models.Contacts>> GetAllContactsAsync();
        Task<ContactsApp.Server.Models.Contacts> GetContactByIdAsync(int id);
        Task<ContactsApp.Server.Models.Contacts> AddContactAsync(ContactsApp.Server.Models.Contacts contact);
        Task<ContactsApp.Server.Models.Contacts> UpdateContactAsync(ContactsApp.Server.Models.Contacts contact);
        Task<bool> DeleteContactAsync(int id);
    }
}
