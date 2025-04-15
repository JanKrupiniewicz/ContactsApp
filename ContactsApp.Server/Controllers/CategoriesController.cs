using ContactsApp.Server.Dtos.Categories;
using ContactsApp.Server.Models;
using ContactsApp.Server.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;

namespace ContactsApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dtos.Categories.CategoriesDto>>> GetAllCategories()
        {
            var categories = await _categoriesService.GetAllCategoriesAsync();
            return Ok(categories);
        }
    }
}
