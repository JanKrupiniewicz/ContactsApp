using ContactsApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="UsersRepository"/> z kontekstem bazy danych.
        /// </summary>
        /// <param name="context"></param>
        public UsersRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Pobiera użytkownika na podstawie jego adresu e-mail.
        /// </summary>
        /// <param name="email">Adres e-mail użytkownika.</param>
        /// <returns>Obiekt użytkownika typu <see cref="Models.Users"/> lub null, jeśli nie znaleziono.</returns>

        public async Task<Models.Users?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        /// <summary>
        /// Dodaje nowego użytkownika do bazy danych.
        /// </summary>
        /// <param name="user">Obiekt użytkownika do dodania.</param>
        /// <returns>Task reprezentujący operację asynchroniczną.</returns>
        public async Task AddUserAsync(Models.Users user)
        {
            await _context.Users.AddAsync(user);
        }
        /// <summary>
        /// Zapisuje zmiany w kontekście bazy danych.
        /// </summary>
        /// <returns>Task reprezentujący operację zapisu zmian.</returns>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
