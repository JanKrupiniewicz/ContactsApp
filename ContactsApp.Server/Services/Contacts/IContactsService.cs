using ContactsApp.Server.Dtos.Contacts;

namespace ContactsApp.Server.Services.Contacts
{
    public interface IContactsService
    {
        Task<List<ContactsCollectionDto>> GetAllContactsAsync();
        Task<List<ContactsCollectionDto>> GetUserContactsAsync(int userId);
        Task<ContactsDetailedDto?> GetContactByIdAsync(int id);
        Task<ContactsDetailedDto> AddContactAsync(ContactsDetailedDto contact);
        Task<ContactsDetailedDto?> UpdateContactAsync(ContactsDetailedDto contact);
        Task<bool> DeleteContactAsync(int id);
    }
}
