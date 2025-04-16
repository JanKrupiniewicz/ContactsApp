namespace ContactsApp.Server.Dtos.Contacts
{
    /// <summary>
    /// Reprezentuje szczegółowe dane kontaktu.
    /// </summary>
    public class ContactsDetailedDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Category { get; set; } = "";
        public string? Subcategory { get; set; }
        public int UserId { get; set; }
    }
}
