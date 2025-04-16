using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Models;
using ContactsApp.Server.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;

namespace ContactsApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="CategoriesController"/> z usługą kategorii.
        /// </summary>
        /// <param name="categoriesService">Serwis kategorii.</param>
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        /// <summary>
        /// Pobiera wszystkie kategorie.
        /// </summary>
        /// <returns>Lista wszystkich kategorii w systemie.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Dtos.Categories.CategoriesDto>>> GetAllCategories()
        {
            var categories = await _categoriesService.GetAllCategoriesAsync();
            return Ok(categories);
        }
    }
}
