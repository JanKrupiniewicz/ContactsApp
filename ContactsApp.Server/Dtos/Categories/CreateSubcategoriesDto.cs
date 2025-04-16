namespace ContactsApp.Server.Dtos.Categories
{
    /// <summary>
    /// Reprezentuje dane do tworzenia podkategorii w systemie.
    /// </summary>
    public class CreateSubcategoriesDto
    {
        public string Name { get; set; } = "";
        public string CategoryName { get; set; } = "";
    }
}
