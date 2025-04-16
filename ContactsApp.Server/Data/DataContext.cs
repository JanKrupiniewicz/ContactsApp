using ContactsApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Data
{
    /// <summary>
    /// Reprezentuje kontekst bazy danych aplikacji.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Contacts> Contacts { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Categories> Categories { get; set; } = null!;
        public DbSet<Subcategories> Subcategories { get; set; } = null!;

        /// <summary>
        /// Inicjalizuje model bazy danych.
        /// </summary>
        /// <param name="modelBuilder">Obiekt służący do konfigurowania modelu.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(u => u.Email).IsUnique();
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Categories>().HasData(
                new Categories { Id = 1, Name = "Private" },
                new Categories { Id = 2, Name = "Business" },
                new Categories { Id = 3, Name = "Other" }
            );

            modelBuilder.Entity<Subcategories>().HasData(
                new Subcategories { Id = 1, Name = "advertising", CategoryId = 2 },
                new Subcategories { Id = 2, Name = "finance", CategoryId = 2 },
                new Subcategories { Id = 3, Name = "marketing", CategoryId = 2 },
                new Subcategories { Id = 4, Name = "sales", CategoryId = 2 },
                new Subcategories { Id = 5, Name = "media", CategoryId = 2 }
            );
        }
    }
}
