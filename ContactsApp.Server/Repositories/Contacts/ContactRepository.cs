
namespace ContactsApp.Server.Repositories.Contacts
{
    public class ContactRepository : IContactRepository
    {
        public Task<Models.Contacts> AddContactAsync(Models.Contacts contact)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Models.Contacts>> GetAllContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Contacts> GetContactByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Contacts> UpdateContactAsync(Models.Contacts contact)
        {
            throw new NotImplementedException();
        }
    }
}
