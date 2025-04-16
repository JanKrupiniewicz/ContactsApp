namespace ContactsApp.Server.Dtos.Contacts
{
    /// <summary>
    /// Reprezentuje dane kontaktu w formacie kolekcji.
    /// </summary>
    public class ContactsCollectionDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
