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

        public ContactsController(IContactsService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactsCollectionDto>>> GetAllContacts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contacts = await _contactService.GetUserContactsAsync(int.Parse(userId));
            return Ok(contacts);
        }

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

        [HttpPost]
        public async Task<ActionResult<ContactsDetailedDto>> AddContact(CreateContactsDto contact)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            contact.UserId = int.Parse(userId);

            var addedContact = await _contactService.AddContactAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = addedContact.Id }, addedContact);
        }

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
