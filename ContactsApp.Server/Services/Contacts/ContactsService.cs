
using AutoMapper;
using ContactsApp.Server.Dtos.Contacts;
using ContactsApp.Server.Models;
using ContactsApp.Server.Repositories.Categories;
using ContactsApp.Server.Repositories.Contacts;

namespace ContactsApp.Server.Services.Contacts
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _repository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public ContactsService(IContactsRepository repository, IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<ContactsCollectionDto>> GetAllContactsAsync()
        {
            var contacts = await _repository.GetAllContactsAsync();
            return _mapper.Map<List<ContactsCollectionDto>>(contacts);
        }

        public async Task<List<ContactsCollectionDto>> GetUserContactsAsync(int userId)
        {
            var contacts = await _repository.GetUserContactsAsync(userId);
            return _mapper.Map<List<ContactsCollectionDto>>(contacts);
        }

        public async Task<ContactsDetailedDto?> GetContactByIdAsync(int id)
        {
            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null) return null;

            var addedContact = _mapper.Map<ContactsDetailedDto>(contact);

            if (contact.Category != null)
            { 
                addedContact.Category = contact.Category.Name;
            }

            if (contact.Subcategory != null)
            {
                addedContact.Subcategory = contact.Subcategory.Name;
            }

            return addedContact;
        }

        public async Task<ContactsDetailedDto> AddContactAsync(CreateContactsDto dto)
        {
            var model = _mapper.Map<Models.Contacts>(dto);

            var category = await _categoriesRepository.GetCategoryByNameAsync(dto.Category);
            if (category != null)
            {
                model.Category = category;

                if (!string.IsNullOrWhiteSpace(dto.Subcategory))
                {
                    var subcategory = await _categoriesRepository.GetSubcategoryByNameAndCategoryIdAsync(dto.Subcategory, category.Id);

                    if (subcategory == null)
                    {
                        // subkategoria nie istnieje — tworzymy nową
                        var newSubcategory = new Subcategories
                        {
                            Name = dto.Subcategory,
                            CategoryId = category.Id
                        };

                        subcategory = await _categoriesRepository.AddSubcategoryAsync(newSubcategory);
                    }

                    model.Subcategory = subcategory;
                }
            }

            var result = await _repository.AddContactAsync(model);

            var addedContact = _mapper.Map<ContactsDetailedDto>(result);
            if (result.Category != null)
            {
                addedContact.Category = result.Category.Name;
            }

            if (result.Subcategory != null)
            {
                addedContact.Subcategory = result.Subcategory.Name;
            }

            return addedContact;
        }

        public async Task<ContactsDetailedDto?> UpdateContactAsync(ContactsDetailedDto dto)
        {
            var existing = await _repository.GetContactByIdAsync(dto.Id);
            if (existing == null) return null;

            var originalUserId = existing.UserId;

            _mapper.Map(dto, existing);
            existing.UserId = originalUserId;

            var category = await _categoriesRepository.GetCategoryByNameAsync(dto.Category);
            existing.Category = category;

            if (!string.IsNullOrEmpty(dto.Subcategory))
            {
                var subcategory = await _categoriesRepository.GetSubcategoryByNameAndCategoryIdAsync(dto.Subcategory, category.Id);

                if (subcategory == null)
                {
                    // subkategoria nie istnieje — tworzymy nową
                    var newSubcategory = new Subcategories
                    {
                        Name = dto.Subcategory,
                        CategoryId = category.Id
                    };

                    subcategory = await _categoriesRepository.AddSubcategoryAsync(newSubcategory);
                }

                existing.Subcategory = subcategory;
            }
            else
            {
                existing.Subcategory = null;
            }

            var result = await _repository.UpdateContactAsync(existing);
            var updatedContact = _mapper.Map<ContactsDetailedDto>(result);

            if (result.Category != null)
            {
                updatedContact.Category = result.Category.Name;
            }

            if (result.Subcategory != null)
            {
                updatedContact.Subcategory = result.Subcategory.Name;
            }

            return updatedContact;
        }


        public Task<bool> DeleteContactAsync(int id)
        {
            return _repository.DeleteContactAsync(id);
        }
    }
}
