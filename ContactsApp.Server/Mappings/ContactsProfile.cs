using AutoMapper;

namespace ContactsApp.Server.Mappings
{
    /// <summary>
    /// Profile do mapowania obiektów związanych z kontaktami.
    /// </summary>
    public class ContactsProfile : Profile
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="ContactsProfile"/> i definiuje mapowania.
        /// </summary>
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
