using mottu_challenge.Dto.Shared;
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

        /// <summary>
        /// Busca uma lista paginada de Usuários.
        /// </summary>
        /// <param name="pageNumber">O número da página a ser retornada (padrão: 1).</param>
        /// <param name="pageSize">A quantidade de itens por página (padrão: 10).</param>
        /// <returns>Uma lista de users.</returns>
        /// <response code="200">Retorna a lista de users com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _context.Users
                            .Where(u => u.FlagAtivo == "S")
                            .OrderBy(u => u.IdUser)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            var usersResponse = _mapper.Map<IEnumerable<UserResponse>>(users);
            return Ok(usersResponse);
        }

        /// <summary>
        /// Busca um user específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do user a ser buscada.</param>
        /// <returns>Os dados do user encontrado.</returns>
        /// <response code="200">Retorna os dados do user com sucesso.</response>
        /// <response code="404">Se o user com o ID especificado não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var userFound = await _context.Users
                .Where(u => u.IdUser == id && u.FlagAtivo == "S")
                .FirstOrDefaultAsync();

            if (userFound == null) return NotFound();

            var userResponse = _mapper.Map<UserResponse>(userFound);

            var selfLink = Url.Action(nameof(GetById), "User", new { id = userResponse.IdUser }, Request.Scheme);
            userResponse.Links.Add(new LinkDto(selfLink, "self", "GET"));

            var updateLink = Url.Action(nameof(Put), "User", new { id = userResponse.IdUser }, Request.Scheme);
            userResponse.Links.Add(new LinkDto(updateLink, "update_user", "PUT"));

            var deleteLink = Url.Action(nameof(Delete), "User", new { id = userResponse.IdUser }, Request.Scheme);
            userResponse.Links.Add(new LinkDto(deleteLink, "delete_user", "DELETE"));

            return Ok(userResponse);
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Motorcycle
        ///     {
        ///        "year": 2024,
        ///        "model": "Honda Titan 160",
        ///        "plate": "XYZ9J87"
        ///     }
        ///
        /// </remarks>
        /// <param name="userRequest">Dados para a criação da nova moto.</param>
        /// <returns>O objeto do user recém-criado, com seu ID e links HATEOAS.</returns>
        /// <response code="201">Retorna usuário recém-criado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> Post(UserRequest userRequest)
        {
            var roleFound = await _context.Roles.FindAsync(userRequest.RoleId);
            if (roleFound == null) return BadRequest();

            var user = _mapper.Map<User>(userRequest);
            user.Role = roleFound;
            user.FlagAtivo = "S";
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userResponse = _mapper.Map<UserResponse>(user);

            var selfLink = Url.Action(nameof(GetById), "User", new { id = userResponse.IdUser }, Request.Scheme);
            userResponse.Links.Add(new LinkDto(selfLink, "self", "GET"));

            var updateLink = Url.Action(nameof(Put), "User", new { id = userResponse.IdUser }, Request.Scheme);
            userResponse.Links.Add(new LinkDto(updateLink, "update_user", "PUT"));

            var deleteLink = Url.Action(nameof(Delete), "User", new { id = userResponse.IdUser }, Request.Scheme);
            userResponse.Links.Add(new LinkDto(deleteLink, "delete_user", "DELETE"));

            return CreatedAtAction(nameof(Get), new { id = userResponse.IdUser }, userResponse);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="id">O ID do usuário a ser atualizada.</param>
        /// <param name="userRequest">Os novos dados para usuário.</param>
        /// <response code="204">Se usuárioo foi atualizado com sucesso.</response>
        /// <response code="404">Se usuário com o ID especificado não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, UserRequest userRequest)
        {
            var userFound = await _context.Users.FindAsync(id);
            var roleFound = await _context.Roles.FindAsync(userRequest.RoleId);

            if (userFound == null) return NotFound();
            if (roleFound == null) return BadRequest();

            _mapper.Map(userRequest, userFound);
            await _context.SaveChangesAsync();
            var updatedUser = _mapper.Map<UserResponse>(userFound);
            return NoContent();
        }

        /// <summary>
        /// Exclui um usuário (soft delete).
        /// </summary>
        /// <param name="id">O ID do usuário a ser excluído.</param>
        /// <response code="204">Se usuário foi excluído com sucesso.</response>
        /// <response code="404">Se usuário com o ID especificado não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
