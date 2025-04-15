using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Category { get; set; } = ""; // służbowy, prywatny, inny

        public string? Subcategory { get; set; } // szef, klient, kolega, rodzina, inny

        [Required]
        public int UserId { get; set; }
    }
}
