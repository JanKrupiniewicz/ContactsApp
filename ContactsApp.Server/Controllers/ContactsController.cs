using ContactsApp.Server.Dtos.Contacts;
using ContactsApp.Server.Services.Contacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactsDetailedDto>> GetContactById(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<ContactsDetailedDto>> AddContact(ContactsDetailedDto contact)
        {
            var addedContact = await _contactService.AddContactAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = addedContact.Id }, addedContact);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContactsDetailedDto>> UpdateContact(int id, ContactsDetailedDto contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }
            var updatedContact = await _contactService.UpdateContactAsync(contact);
            if (updatedContact == null)
            {
                return NotFound();
            }
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
