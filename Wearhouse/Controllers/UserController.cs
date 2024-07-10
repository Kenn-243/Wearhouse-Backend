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
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<IEnumerable<GetUserResult>>> Get()
        {
            var users = await _context.User
                .OrderBy(x => x.UserId)
                .Select(x => new GetUserResult()
                {
                    UserId = x.UserId,
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                })
                .ToListAsync();

            var response = new APIResponse<IEnumerable<GetUserResult>>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = users
            };
            return Ok(response);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<GetUserResult>> GetUserById(int UserId)
        {
            var user = await _context.User
                .Where(x => x.UserId == UserId)
                .OrderBy(x => x.UserId)
                .Select(x => new GetUserResult()
                {
                    UserId = x.UserId,
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                })
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound("User Not Found");
            }

            var response = new APIResponse<GetUserResult>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = user
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] CreateUserRequest createUserRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkUser = await _context.User.FirstOrDefaultAsync(x => x.UserId == createUserRequest.UserId);

            if(checkUser != null)
            {
                return NotFound("User Already Exist");
            }

            var user = new User()
            {
                UserId = createUserRequest.UserId,
                Username = createUserRequest.Username,
                Email = createUserRequest.Email,
                Password = createUserRequest.Password,
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("PutUser")]
        public async Task<IActionResult> Put(int UserId, [FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == UserId);

            if(user == null)
            {
                return NotFound("User Not Found");
            }

            user.Username = updateUserRequest.Username;
            user.Email = updateUserRequest.Email;
            user.Password = updateUserRequest.Password;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(int UserId)
        {
            var student = await _context.User.FirstOrDefaultAsync(x => x.UserId == UserId);

            if(student == null)
            {
                return NotFound("User not found");
            }

            _context.User.Remove(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
