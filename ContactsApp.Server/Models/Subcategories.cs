using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Server.Models
{
    /// <summary>
    /// Reprezentuje podkategorię w systemie.
    /// </summary>
    public class Subcategories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }
    }
}
