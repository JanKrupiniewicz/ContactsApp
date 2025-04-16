using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Server.Models
{
    /// <summary>
    /// Reprezentuje kategorię w systemie.
    /// </summary>
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public ICollection<Subcategories>? Subcategories { get; set; }
    }
}
