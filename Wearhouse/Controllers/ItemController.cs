using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wearhouse.Data;
using Wearhouse.Models;
using Wearhouse.Models.Request;
using Wearhouse.Models.Result;

namespace Wearhouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ItemController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetItem")]
        public async Task<ActionResult<IEnumerable<GetItemResult>>> Get()
        {
            var items = await _context.Item
                .OrderBy(x => x.ItemId)
                .Select(x => new GetItemResult()
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    UserId = x.UserId,
                })
                .ToListAsync();

            var response = new APIResponse<IEnumerable<GetItemResult>>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = items
            };
            return Ok(response);
        }

        [HttpGet("GetItemById")]
        public async Task<ActionResult<GetItemResult>> GetItemById(int ItemId)
        {
            var item = await _context.Item
                .Where(x => x.ItemId == ItemId)
                .OrderBy(x => x.ItemId)
                .Select(x => new GetItemResult()
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    UserId = x.UserId,
                })
                .FirstOrDefaultAsync();

            if(item == null)
            {
                return NotFound("Item Not Found");
            }

            var response = new APIResponse<GetItemResult>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = item
            };
            return Ok(response);
        }

        [HttpPost("PostItem")]
        public async Task<IActionResult> Post([FromBody] CreateItemRequest createItemRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == createItemRequest.ItemId);

            if(checkItem != null)
            {
                return NotFound("Item Already Exist");
            }

            var item = new Item()
            {
                ItemId = createItemRequest.ItemId,
                ItemName = createItemRequest.ItemName,
                UserId = createItemRequest.UserId,
            };

            _context.Item.Add(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("PutItem")]
        public async Task<IActionResult> Put(int ItemId, [FromBody] UpdateItemRequest updateItemRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == ItemId);

            if(item == null)
            {
                return NotFound("Item Not Found");
            }

            item.ItemName = updateItemRequest.ItemName;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> Delete(int ItemId)
        {
            var item = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == ItemId);

            if(item == null)
            {
                return NotFound("Item not found");
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
