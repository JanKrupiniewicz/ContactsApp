using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Server.Models
{
    public class Contacts
    {
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
        public string Phone { get; set; } = "";

        public DateTime BirthDate { get; set; }

        [Required]
        public string Category { get; set; } = ""; // służbowy, prywatny, inny

        public string? Subcategory { get; set; }

        public string? UserId { get; set; }

    }
}
