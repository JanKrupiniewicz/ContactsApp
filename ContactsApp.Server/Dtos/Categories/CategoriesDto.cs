namespace ContactsApp.Server.Dtos.Categories
{
    /// <summary>
    /// Reprezentuje kategorię w systemie.
    /// </summary>
    public class CategoriesDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public List<SubcategoriesDto>? Subcategories { get; set; } = new List<SubcategoriesDto>();
    }
}
