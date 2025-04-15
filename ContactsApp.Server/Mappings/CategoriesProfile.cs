using AutoMapper;
using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Models;

namespace ContactsApp.Server.Mappings
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile() 
        { 
            CreateMap<Categories, CategoriesDto>().ReverseMap();
            CreateMap<Subcategories, SubcategoriesDto>().ReverseMap();
        }
    }
}
