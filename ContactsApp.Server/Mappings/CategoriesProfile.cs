using AutoMapper;
using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Models;

namespace ContactsApp.Server.Mappings
{
    /// <summary>
    /// Profile do mapowania obiektów związanych z kategoriami i subkategoriami.
    /// </summary>
    public class CategoriesProfile : Profile
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="CategoriesProfile"/> i definiuje mapowania.
        /// </summary>
        public CategoriesProfile() 
        { 
            CreateMap<Categories, CategoriesDto>().ReverseMap();
            CreateMap<Subcategories, SubcategoriesDto>().ReverseMap();
        }
    }
}
