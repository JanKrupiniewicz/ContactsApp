﻿
using ContactsApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Server.Repositories.Contacts
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly DataContext _context;

        public ContactsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Contacts>> GetAllContactsAsync()
        {
            return await _context.Contacts.Include(c => c.Category).Include(c => c.Subcategory).ToListAsync();
        }

        public async Task<List<Models.Contacts>> GetUserContactsAsync(int userId)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId).Include(c => c.Category).Include(c => c.Subcategory).ToListAsync();
        }

        public async Task<Models.Contacts> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.Include(c => c.Category).Include(c => c.Subcategory).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Models.Contacts> AddContactAsync(Models.Contacts contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Models.Contacts> UpdateContactAsync(Models.Contacts contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

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
