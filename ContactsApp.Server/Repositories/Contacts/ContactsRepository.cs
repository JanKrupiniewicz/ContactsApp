
using ContactsApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Repositories.Contacts
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly DataContext _context;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="ContactsRepository"/> z kontekstem bazy danych.
        /// </summary>
        /// <param name="context"></param>
        public ContactsRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Pobiera wszystkie kontakty z bazy danych.
        /// </summary>
        /// <returns>Lista kontaktów typu <see cref="Models.Contacts"/>.</returns>
        public async Task<List<Models.Contacts>> GetAllContactsAsync()
        {
            return await _context.Contacts.Include(c => c.Category).Include(c => c.Subcategory).ToListAsync();
        }
        /// <summary>
        /// Pobiera kontakty użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Lista kontaktów użytkownika typu <see cref="Models.Contacts"/>.</returns>
        public async Task<List<Models.Contacts>> GetUserContactsAsync(int userId)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId).Include(c => c.Category).Include(c => c.Subcategory).ToListAsync();
        }
        /// <summary>
        /// Pobiera kontakt na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator kontaktu.</param>
        /// <returns>Obiekt kontaktu typu <see cref="Models.Contacts"/> lub null, jeśli nie znaleziono.</returns>
        public async Task<Models.Contacts> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.Include(c => c.Category).Include(c => c.Subcategory).FirstOrDefaultAsync(c => c.Id == id);
        }
        /// <summary>
        /// Dodaje nowy kontakt do bazy danych.
        /// </summary>
        /// <param name="contact">Obiekt kontaktu do dodania.</param>
        /// <returns>Zwraca dodany kontakt typu <see cref="Models.Contacts"/>.</returns>
        public async Task<Models.Contacts> AddContactAsync(Models.Contacts contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
        /// <summary>
        /// Aktualizuje istniejący kontakt w bazie danych.
        /// </summary>
        /// <param name="contact">Zaktualizowany obiekt kontaktu.</param>
        /// <returns>Zwraca zaktualizowany kontakt typu <see cref="Models.Contacts"/>.</returns>
        public async Task<Models.Contacts> UpdateContactAsync(Models.Contacts contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
        /// <summary>
        /// Usuwa kontakt z bazy danych na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator kontaktu do usunięcia.</param>
        /// <returns>True, jeśli usunięcie się powiodło, w przeciwnym razie false.</returns>
        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return false;
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
