using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UptownShopperApiV2.Models;
using UptownShopperApiV2.Repository;

namespace UptownShopperApiV2.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly ItemRepository _itemRepository;

        public ItemController(IConfiguration configuration)
        {
            _itemRepository = new ItemRepository(configuration);
        }

        // GET: api/item
        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            return _itemRepository.GetAll();
        }
        
        // GET: api/item/active
        [HttpGet("{active}", Name = "GetAllActive")]
        public IEnumerable<Item> GetAllActive(bool active)
        {
            return _itemRepository.GetAllActive(active);
        }

        // GET: api/item/id
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetById(long id)
        {
            var item = _itemRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST: api/item
        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var newItemId = _itemRepository.Add(item);
            item.Id = newItemId;
            
            return CreatedAtRoute(
                routeName: "GetItem",
                routeValues: new { id = newItemId },
                value: item
            );
        }

        // PUT: api/item/id
        [HttpPut("{id}")] 
        public IActionResult Update(long id, [FromBody] Item item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var existingItem = _itemRepository.Find(item.Id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _itemRepository.Update(item);

            return new NoContentResult(); 
        }

        // need to modify to send logged in user to db for "Deleted By"
        // Delete: api/item/id
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _itemRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _itemRepository.Remove(id);
            return new NoContentResult();
        }
    }
}