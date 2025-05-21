using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Model;

namespace mottu_challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        
        
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post(UserDto user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserDto user)
        {
            if (id != user.Id) return BadRequest();
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var found = await _context.Users.FindAsync(id);
            return Ok(found);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var foundUser = await _context.Users.FindAsync(id);
            if (foundUser == null) return NotFound();

            _context.Users.Remove(foundUser);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
