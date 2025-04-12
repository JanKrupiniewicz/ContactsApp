using ContactsApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<ContactsApp.Server.Models.Contacts> Contacts { get; set; } = null!;
        public DbSet<ContactsApp.Server.Models.Users> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(u => u.Email).IsUnique();
            base.OnModelCreating(modelBuilder); 
        }
    }
}
