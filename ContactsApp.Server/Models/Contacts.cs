using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Server.Models
{
    public class Contacts
    {
        private int Id { get; set; }
        [Required]
        private string FirstName { get; set; } = "";

        [Required]
        private string LastName { get; set; } = "";

        [Required, EmailAddress]
        private string Email { get; set; } = "";

        [Required]
        private string Password { get; set; } = "";

        [Phone]
        private string Phone { get; set; } = "";

        private DateTime BirthDate { get; set; }

        [Required]
        private string Category { get; set; } = ""; // służbowy, prywatny, inny

        private string? Subcategory { get; set; }

        private string? UserId { get; set; }

    }
}
