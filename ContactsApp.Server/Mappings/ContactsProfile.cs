using AutoMapper;

namespace ContactsApp.Server.Mappings
{
    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<Models.Contacts, Dtos.Contacts.ContactsDetailedDto>()
                .ReverseMap();
            CreateMap<Models.Contacts, Dtos.Contacts.ContactsCollectionDto>()
                .ReverseMap();
            CreateMap<Models.Contacts, Dtos.Contacts.CreateContactsDto>().ReverseMap();
        }
    }
}
