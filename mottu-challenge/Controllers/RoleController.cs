using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Dto.Request;
using mottu_challenge.Dto.Response;
using mottu_challenge.Model;

namespace mottu_challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleController(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var roles = await _context.Roles
                .Where(r => r.FlagAtivo == "S")
                .ToListAsync();
            if (roles == null) return NotFound();

            var rolesReponse = _mapper.Map<IEnumerable<RoleResponse>>(roles);
            return Ok(rolesReponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var roleFound = await _context.Roles
                .Where(r => r.IdRole == id && r.FlagAtivo == "S")
                .FirstOrDefaultAsync();
            if (roleFound == null) return NotFound();

            var roleResponse = _mapper.Map<RoleResponse>(roleFound);
            return Ok(roleResponse);
        }

        [HttpPost]
        public async Task<ActionResult> Post(RoleRequest roleRequest)
        {
            var role = _mapper.Map<Role>(roleRequest);
            _context.Roles.Add(role);
            role.FlagAtivo = "S";
            role.CreatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            var roleResponse = _mapper.Map<RoleResponse>(role);
            return CreatedAtAction(nameof(GetById), new {id = roleResponse.IdRole}, roleResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, RoleRequest roleRequest)
        {
            var roleFound = await _context.Roles.FindAsync(id);
            if (roleFound == null) return NotFound();

            _mapper.Map(roleRequest, roleFound);

            await _context.SaveChangesAsync();

            var updatedRole = _mapper.Map<RoleResponse>(roleFound);
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var roleFound = await _context.Roles.FindAsync(id);
            if (roleFound == null) return BadRequest();

            roleFound.FlagAtivo = "N";
        
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }   
}
