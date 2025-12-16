using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Application.Dtos.ContactDtos;
using MultiShop.Catalog.Application.Services.ContactService;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var response = await _contactService.GetAllContactAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(string id)
        {
            var response = await _contactService.GetByIdContactAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var response = await _contactService.CreateContactAsync(createContactDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var response=await _contactService.DeleteContactAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateContactDto updateContactDto)
        {
            var response = await _contactService.UpdateContactAsync(updateContactDto);
            return StatusCode(response.StatusCode, response);
        }

    }
}
