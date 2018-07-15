using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UptownShopperApiV2.Models;
using UptownShopperApiV2.Repository;

namespace UptownShopperApiV2.Controllers
{
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly StoreRepository _storeRepository;

        public StoreController(IConfiguration configuration)
        {
            _storeRepository = new StoreRepository(configuration);
        }

        // GET: api/store
        [HttpGet]
        public IEnumerable<Store> GetAll()
        {
            return _storeRepository.GetAll();
        }

        // GET: api/store/id
        [HttpGet("{id}", Name = "GetStore")]
        public IActionResult GetById(long id)
        {
            var store = _storeRepository.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return new ObjectResult(store);
        }

        // GET: api/store/id/item
        [HttpGet("{id}/item", Name = "GetStoreItems")]
        public IActionResult GetItemsByStoreId(long id)
        {
            var item = _storeRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }
        
        // POST: api/store
        [HttpPost]
        public IActionResult Create([FromBody] Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }

            var newStoreId = _storeRepository.Add(store);
            store.Id = newStoreId;
            
            return CreatedAtRoute(
                routeName: "GetStore",
                routeValues: new { id = newStoreId },
                value: store
            );
        }

        // PUT: api/store/id
        [HttpPut("{id}")] 
        public IActionResult Update(long id, [FromBody] Store store)
        {
            if (store == null || store.Id != id)
            {
                return BadRequest();
            }

            var existingStore = _storeRepository.Find(store.Id);
            if (existingStore == null)
            {
                return NotFound();
            }

            _storeRepository.Update(store);

            return new NoContentResult(); 
        }

        // Delete: api/store/id
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var store = _storeRepository.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            _storeRepository.Remove(id);
            return new NoContentResult();
        }
    }
}