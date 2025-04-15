using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsApp.Server.Models
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Phone]
        public string PhoneNumber { get; set; } = "";

        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }

        [ForeignKey("Subcategory")]
        public int? SubcategoryId { get; set; }
        public Subcategories? Subcategory { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public Users? User { get; set; }
    }
}
