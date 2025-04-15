using AutoMapper;
using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Repositories.Categories;

namespace ContactsApp.Server.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriesDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoriesRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoriesDto>>(categories);
        }
    }
}
