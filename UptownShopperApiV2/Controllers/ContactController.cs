using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UptownShopperApiV2.Models;
using UptownShopperApiV2.Repository;

namespace UptownShopperApiV2.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactRepository _contactRepository;

        public ContactController(IConfiguration configuration)
        {
            _contactRepository = new ContactRepository(configuration);
        }


        // GET: /contact
        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

        // GET: /contact/id
        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult GetById(long id)
        {
            var contact = _contactRepository.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return new ObjectResult(contact);
        }

        // POST: /contact
        [HttpPost]
        public IActionResult Create([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            var newContactId = _contactRepository.Add(contact);
            contact.Id = newContactId;

            return CreatedAtRoute(
                routeName: "GetContact", 
                routeValues: new { id = newContactId }, 
                value: contact
            );
        }

        // PUT: /Contact/id
        [HttpPut("{id}")] 
        public IActionResult Update(long id, [FromBody] Contact contact)
        {
            if (contact == null || contact.Id != id)
            {
                return BadRequest();
            }

            var existingContact = _contactRepository.Find(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            _contactRepository.Update(contact);

            return new NoContentResult(); 
        }

        // Delete: /contact/id
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var contact = _contactRepository.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _contactRepository.Remove(id);
            return new NoContentResult();
        }
    }
}