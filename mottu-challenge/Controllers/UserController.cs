using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Dto.Request;
using mottu_challenge.Dto.Response;
using mottu_challenge.Model;

namespace mottu_challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var users = await _context.Users.ToListAsync();
            var usersResponse = _mapper.Map<IEnumerable<UserResponse>>(users);
            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var userFound = await _context.Users
                .Where(u => u.IdUser == id && u.FlagAtivo == "S")
                .FirstOrDefaultAsync();
                                      
            if (userFound == null) return NotFound();
            var userResponse = _mapper.Map<UserResponse>(userFound);
            return Ok(userResponse);    
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserRequest userRequest)
        {

            var roleFound = await _context.Roles.FindAsync(userRequest.RoleId);
            if (roleFound == null) return BadRequest();
            
            var user = _mapper.Map<User>(userRequest);
            user.Role = roleFound;
            if (string.IsNullOrWhiteSpace(user.FlagAtivo))
            {
                user.FlagAtivo = "S";
            }
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userResponse = _mapper.Map<UserResponse>(user);

            return CreatedAtAction(nameof(Get), new {id = userResponse.IdUser}, userResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserRequest userRequest)
        {;
         
            var userFound = await _context.Users.FindAsync(id);
            var roleFound = await _context.Roles.FindAsync(userRequest.RoleId);

            if (userFound == null) return NotFound();
            if (roleFound == null) return BadRequest();

            var user = _mapper.Map<User>(userRequest);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var foundUser = await _context.Users.FindAsync(id);
            if (foundUser == null) return NotFound();

            foundUser.FlagAtivo = "N";
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
