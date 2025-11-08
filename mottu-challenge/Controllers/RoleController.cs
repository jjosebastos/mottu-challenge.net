using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Dto.Request;
using mottu_challenge.Dto.Response;
using mottu_challenge.Dto.Shared;
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

        /// <summary>
        /// Busca uma lista paginada de perfis (Roles).
        /// </summary>
        /// <param name="pageNumber">O número da página a ser retornada (padrão: 1).</param>
        /// <param name="pageSize">A quantidade de itens por página (padrão: 10).</param>
        /// <returns>Uma lista de perfis.</returns>
        /// <response code="200">Retorna a lista de perfis com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var roles = await _context.Roles
                .Where(r => r.FlagAtivo == "S")
                .OrderBy(r => r.IdRole)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var rolesResponse = _mapper.Map<IEnumerable<RoleResponse>>(roles);
            return Ok(rolesResponse);
        }

        /// <summary>
        /// Busca um perfil específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do perfil a ser buscado.</param>
        /// <returns>Os dados do perfil encontrado.</returns>
        /// <response code="200">Retorna os dados do perfil com sucesso.</response>
        /// <response code="404">Se o perfil com o ID especificado não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var roleFound = await _context.Roles
                .Where(r => r.IdRole == id && r.FlagAtivo == "S")
                .FirstOrDefaultAsync();

            if (roleFound == null) return NotFound();

            var roleResponse = _mapper.Map<RoleResponse>(roleFound);

            var selfLink = Url.Action(nameof(GetById), "Role", new { id = roleResponse.IdRole }, Request.Scheme);
            roleResponse.Links.Add(new LinkDto(selfLink, "self", "GET"));
            var updateLink = Url.Action(nameof(Put), "Role", new { id = roleResponse.IdRole }, Request.Scheme);
            roleResponse.Links.Add(new LinkDto(updateLink, "update_role", "PUT"));
            var deleteLink = Url.Action(nameof(Delete), "Role", new { id = roleResponse.IdRole }, Request.Scheme);
            roleResponse.Links.Add(new LinkDto(deleteLink, "delete_role", "DELETE"));

            return Ok(roleResponse);
        }

        /// <summary>
        /// Cria um novo perfil no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Role
        ///     {
        ///        "roleName": "Entregador",
        ///        "roleDescription": "Perfil para usuários que são entregadores."
        ///     }
        ///
        /// </remarks>
        /// <param name="roleRequest">Dados para a criação do novo perfil.</param>
        /// <returns>O objeto do perfil recém-criado, com seu ID e links HATEOAS.</returns>
        /// <response code="201">Retorna o perfil recém-criado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(RoleRequest roleRequest)
        {
            var role = _mapper.Map<Role>(roleRequest);
            _context.Roles.Add(role);
            role.FlagAtivo = "S";
            role.CreatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var roleResponse = _mapper.Map<RoleResponse>(role);

            var selfLink = Url.Action(nameof(GetById), "Role", new { id = roleResponse.IdRole }, Request.Scheme);
            roleResponse.Links.Add(new LinkDto(selfLink, "self", "GET"));
            var updateLink = Url.Action(nameof(Put), "Role", new { id = roleResponse.IdRole }, Request.Scheme);
            roleResponse.Links.Add(new LinkDto(updateLink, "update_role", "PUT"));
            var deleteLink = Url.Action(nameof(Delete), "Role", new { id = roleResponse.IdRole }, Request.Scheme);
            roleResponse.Links.Add(new LinkDto(deleteLink, "delete_role", "DELETE"));

            return CreatedAtAction(nameof(GetById), new { id = roleResponse.IdRole }, roleResponse);
        }

        /// <summary>
        /// Atualiza os dados de um perfil existente.
        /// </summary>
        /// <param name="id">O ID do perfil a ser atualizado.</param>
        /// <param name="roleRequest">Os novos dados para o perfil.</param>
        /// <response code="204">Se o perfil foi atualizado com sucesso.</response>
        /// <response code="404">Se o perfil com o ID especificado não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, RoleRequest roleRequest)
        {
            var roleFound = await _context.Roles.FindAsync(id);
            if (roleFound == null) return NotFound();

            _mapper.Map(roleRequest, roleFound);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Exclui um perfil (soft delete).
        /// </summary>
        /// <param name="id">O ID do perfil a ser excluído.</param>
        /// <response code="204">Se o perfil foi excluído com sucesso.</response>
        /// <response code="404">Se o perfil com o ID especificado não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var roleFound = await _context.Roles.FindAsync(id);
            if (roleFound == null) return NotFound();

            roleFound.FlagAtivo = "N";
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}