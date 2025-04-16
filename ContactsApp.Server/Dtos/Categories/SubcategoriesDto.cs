using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Server.Dtos.Categories
{
    /// <summary>
    /// Reprezentuje podkategorię w systemie.
    /// </summary>
    public class SubcategoriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int CategoryId { get; set; }
    }
}
