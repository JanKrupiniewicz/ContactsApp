using ContactsApp.Server.Dtos.Contacts;
using ContactsApp.Server.Services.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactService;
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="ContactsController"/> z usługą kontaktów.
        /// </summary>
        /// <param name="contactService">Serwis kontaktów.</param>
        public ContactsController(IContactsService contactService)
        {
            _contactService = contactService;
        }
        /// <summary>
        /// Pobiera wszystkie kontakty użytkownika.
        /// </summary>
        /// <returns>Lista kontaktów przypisanych do aktualnie zalogowanego użytkownika.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ContactsCollectionDto>>> GetAllContacts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contacts = await _contactService.GetUserContactsAsync(int.Parse(userId));
            return Ok(contacts);
        }
        /// <summary>
        /// Pobiera kontakt na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator kontaktu.</param>
        /// <returns>Szczegóły kontaktu, jeśli istnieje i należy do użytkownika.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactsDetailedDto>> GetContactById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var contact = await _contactService.GetContactByIdAsync(id);

            if (contact == null || contact.UserId != int.Parse(userId))
            {
                return NotFound(); // contact not found or not owned by user
            }

            return Ok(contact);
        }
        /// <summary>
        /// Dodaje nowy kontakt do bazy danych.
        /// </summary>
        /// <param name="contact">Dane kontaktu do dodania.</param>
        /// <returns>Utworzony kontakt wraz z jego identyfikatorem.</returns>
        [HttpPost]
        public async Task<ActionResult<ContactsDetailedDto>> AddContact(CreateContactsDto contact)
        {

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                contact.UserId = int.Parse(userId);

                var addedContact = await _contactService.AddContactAsync(contact);
                return CreatedAtAction(nameof(GetContactById), new { id = addedContact.Id }, addedContact);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Aktualizuje istniejący kontakt w bazie danych.
        /// </summary>
        /// <param name="id">Identyfikator kontaktu do zaktualizowania.</param>
        /// <param name="contact">Zaktualizowane dane kontaktu.</param>
        /// <returns>Zaktualizowany kontakt.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactsDetailedDto>> UpdateContact(int id, ContactsDetailedDto contact)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (id != contact.Id)
            {
                return BadRequest();
            }

            var existing = await _contactService.GetContactByIdAsync(id);
            if (existing == null || existing.UserId != int.Parse(userId))
            {
                return NotFound();
            }

            contact.UserId = int.Parse(userId); // ensure assignment
            var updatedContact = await _contactService.UpdateContactAsync(contact);
            return Ok(updatedContact);
        }
        /// <summary>
        /// Usuwa kontakt na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator kontaktu do usunięcia.</param>
        /// <returns>Status operacji usunięcia.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null || contact.UserId != int.Parse(userId))
            { 
                return NotFound();
            }

            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
            { 
                return StatusCode(500, "Failed to delete contact");
            }

            return NoContent();
        }
    }
}
