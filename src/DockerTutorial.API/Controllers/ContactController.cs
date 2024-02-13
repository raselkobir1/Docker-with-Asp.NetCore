using DockerTutorial.BLL.Services;
using DockerTutorial.DLL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DockerTutorial.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) {
            _contactService = contactService;
        }


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _contactService.GetAllContact());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Contact addContactRequest)
        {
            
            return Ok(await _contactService.SaveContact(addContactRequest));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            
            return Ok(await _contactService.GetAContact(id));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int id, Contact updateContactRequest)
        {
            return Ok(await _contactService.UpdateContact(id, updateContactRequest));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            return Ok(await _contactService.DeleteContact(id));
        }
    }
}
