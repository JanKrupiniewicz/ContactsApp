using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<ContactsApp.Server.Models.Contacts> Contacts { get; set; } = null!;
    }
}
