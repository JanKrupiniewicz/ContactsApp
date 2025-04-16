using AutoMapper;
using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Repositories.Categories;

namespace ContactsApp.Server.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="ContactsService"/>.
        /// </summary>
        /// <param name="categoriesRepository">Repozytorium kategori.</param>
        /// <param name="mapper">Mapper do mapowania obiektów.</param>
        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Pobiera listę wszystkich kontaktów.
        /// </summary>
        /// <returns>Lista kategorii w formacie <see cref="CategoriesDto"/>.</returns>
        public async Task<List<CategoriesDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoriesRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoriesDto>>(categories);
        }
    }
}
