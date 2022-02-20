using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Models;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroceriesController : ControllerBase
    {
        private readonly ILogger<GroceriesController> _logger;
        private GroceriesContext _context;

        public GroceriesController(ILogger<GroceriesController> logger, GroceriesContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("items/{storeId}")]
        public async Task<ActionResult<IReadOnlyList<Item>>> GetItems(int storeId)
        {
            if(storeId <= 0)
                return BadRequest();
            
            var items = await _context.Items.Where(x => x.StoreId == storeId).ToListAsync();

            if(items.Any())
            {
                return items;
            }

            return NotFound();
        }

        [HttpGet("stores")]
        public async Task<IReadOnlyList<Store>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        [HttpGet("stores/short")]
        public async Task<IReadOnlyList<object>> GetStoreNames()
        {
            return await _context.Stores.Select(x => new { StoreId = x.StoreId, Name = x.Name }).ToListAsync();
        }

        [HttpPatch("update/store/item")]
        public async Task<ActionResult> UpdateItemsInStore([FromBody] Item item)
        {
            if(ModelState.IsValid)
            {
                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpPatch("update/store/{storeId}/description")]
        public async Task<ActionResult> UpdateStoreDescriptionByStoreId([FromBody] string desc, [FromRoute] int storeId)
        {
            if(ModelState.IsValid)
            {
                var storeToUpdate = await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == storeId);

                if(storeToUpdate != null)
                {
                    storeToUpdate.Description = desc;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }

        [HttpPut("update/item")]
        public async Task<ActionResult> UpdateItem([FromBody] Item item)
        {
            if(ModelState.IsValid)
            {
                var itemToUpdate = await _context.Items.FirstOrDefaultAsync(x => x.Id == item.Id);

                if(itemToUpdate != null)
                {
                    itemToUpdate.Name = item.Name;
                    itemToUpdate.Description = item.Description;
                    itemToUpdate.Amount = item.Amount;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }

        [HttpDelete("delete/item/{id}")]
        public async Task DeleteItem([FromRoute] int id)
        {
            var _item = await _context.Items.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            if(_item != null)
            {
                _context.Items.Remove(_item);
                await _context.SaveChangesAsync();
            }
        }

        [HttpGet("store/{storeId}")]
        public async Task<Store> GetStoreById(int storeId)
        {
            return await _context.Stores.Where(x => x.StoreId == storeId).Include(x => x.Items).FirstOrDefaultAsync();
        }

        [HttpDelete("delete/store/{storeId}")]
        public async Task DeleteStore([FromRoute] int storeId)
        {
            var _store = await _context.Stores.Where(x => x.StoreId.Equals(storeId)).FirstOrDefaultAsync();
            if(_store != null)
            {
                _context.Stores.Remove(_store);
                await _context.SaveChangesAsync();
            }
        }

        [HttpPost("add/store")]
        public async Task<ActionResult> AddStore([FromBody] Store store)
        {
            if(ModelState.IsValid)
            {
                await _context.Stores.AddAsync(store);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update/store")]
        public async Task<ActionResult> UpdateStore([FromBody] Store store)
        {
            if(ModelState.IsValid)
            {
                var storeToUpdate = await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == store.StoreId);

                if(storeToUpdate != null)
                {
                    storeToUpdate.Name = store.Name;
                    storeToUpdate.Description = store.Description;
                    storeToUpdate.Purpose = store.Purpose;
                    storeToUpdate.Items = store.Items;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("update/items/{storeId}")]
        public async Task<ActionResult> UpdateItem([FromRoute] int storeId, [FromBody] Item[] items)
        {
            if(storeId > 1)
            {
                var storeToUpdate = await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == storeId);

                if(storeToUpdate != null)
                {
                    storeToUpdate.Items = items;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }


    }
}
