using AutoMapper;

namespace ContactsApp.Server.Mappings
{
    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<Models.Contacts, Dtos.Contacts.ContactsDetailedDto>()
                .ReverseMap().ForMember(dest => dest.Category, opt => opt.Ignore()).ForMember(dest => dest.Subcategory, opt => opt.Ignore());
            CreateMap<Models.Contacts, Dtos.Contacts.ContactsCollectionDto>()
                .ReverseMap();
            CreateMap<Models.Contacts, Dtos.Contacts.CreateContactsDto>().ReverseMap().ForMember(dest => dest.Category, opt => opt.Ignore()).ForMember(dest => dest.Subcategory, opt => opt.Ignore());
        }
    }
}
