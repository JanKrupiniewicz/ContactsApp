
using AutoMapper;
using ContactsApp.Server.Dtos.Contacts;
using ContactsApp.Server.Repositories.Contacts;

namespace ContactsApp.Server.Services.Contacts
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _repository;
        private readonly IMapper _mapper;

        public ContactsService(IContactsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ContactsCollectionDto>> GetAllContactsAsync()
        {
            var contacts = await _repository.GetAllContactsAsync();
            return _mapper.Map<List<ContactsCollectionDto>>(contacts);
        }

        public async Task<ContactsDetailedDto?> GetContactByIdAsync(int id)
        {
            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null) return null;

            return _mapper.Map<ContactsDetailedDto>(contact);
        }

        public async Task<ContactsDetailedDto> AddContactAsync(ContactsDetailedDto dto)
        {
            var model = _mapper.Map<Models.Contacts>(dto);
            var result = await _repository.AddContactAsync(model);

            var addedContact = _mapper.Map<ContactsDetailedDto>(result);
            return addedContact;
        }

        public async Task<ContactsDetailedDto?> UpdateContactAsync(ContactsDetailedDto dto)
        {
            var existing = await _repository.GetContactByIdAsync(dto.Id);
            if (existing == null) return null;

            var model = _mapper.Map<Models.Contacts>(dto);
            var result = await _repository.UpdateContactAsync(model);
            var updatedContact = _mapper.Map<ContactsDetailedDto>(result);
            return updatedContact;
        }

        public Task<bool> DeleteContactAsync(int id)
        {
            return _repository.DeleteContactAsync(id);
        }
    }
}
