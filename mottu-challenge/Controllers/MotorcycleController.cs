using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Dto;
using mottu_challenge.Dto.Request;
using mottu_challenge.Dto.Response;
using mottu_challenge.Dto.Shared;
using mottu_challenge.Model;

namespace mottu_challenge.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MotorcycleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public MotorcycleController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca uma lista paginada de motos.
        /// </summary>
        /// <param name="pageNumber">O número da página a ser retornada (padrão: 1).</param>
        /// <param name="pageSize">A quantidade de itens por página (padrão: 10).</param>
        /// <returns>Uma lista de motos.</returns>
        /// <response code="200">Retorna a lista de motos com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<MotorcycleResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<MotorcycleResponse>>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // 1. Contagem total (precisa ser feita ANTES do Skip/Take)
            var totalRecords = await _context.Motorcycles
                .Where(m => m.FlagAtivo == "S")
                .CountAsync();

            // 2. Busca paginada
            var motorcycles = await _context.Motorcycles
                .Where(m => m.FlagAtivo == "S")
                .OrderBy(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var motorcycleResponseList = _mapper.Map<List<MotorcycleResponse>>(motorcycles);
            foreach (var moto in motorcycleResponseList)
            {
                var selfLink = Url.Action(nameof(GetById), "Motorcycle", new { id = moto.Id }, Request.Scheme);
                moto.Links.Add(new LinkDto(selfLink, "self", "GET"));
            }
            var pagedResponse = new PagedResponse<MotorcycleResponse>(motorcycleResponseList, pageNumber, pageSize, totalRecords);
            pagedResponse.Links.Add(new LinkDto(
                Url.Action(nameof(Get), "Motorcycle", new { pageNumber, pageSize }, Request.Scheme),
                "self", "GET"));

            // Link para a próxima página (se existir)
            if (pageNumber < pagedResponse.TotalPages)
            {
                pagedResponse.Links.Add(new LinkDto(
                    Url.Action(nameof(Get), "Motorcycle", new { pageNumber = pageNumber + 1, pageSize }, Request.Scheme),
                    "next-page", "GET"));
            }
            if (pageNumber > 1)
            {
                pagedResponse.Links.Add(new LinkDto(
                    Url.Action(nameof(Get), "Motorcycle", new { pageNumber = pageNumber - 1, pageSize }, Request.Scheme),
                    "previous-page", "GET"));
            }

            return Ok(pagedResponse);
        }

        /// <summary>
        /// Busca uma moto específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da moto a ser buscada.</param>
        /// <returns>Os dados da moto encontrada.</returns>
        /// <response code="200">Retorna os dados da moto com sucesso.</response>
        /// <response code="404">Se a moto com o ID especificado não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotorcycleResponse>> GetById(int id)
        {
            var motorcycle = await _context.Motorcycles
                .FirstOrDefaultAsync(m => m.Id == id && m.FlagAtivo == "S");

            if (motorcycle == null)
            {
                return NotFound();
            }

            var motorcycleResponse = _mapper.Map<MotorcycleResponse>(motorcycle);

            var selfLink = Url.Action(nameof(GetById), "Motorcycle", new { id = motorcycleResponse.Id }, Request.Scheme);
            motorcycleResponse.Links.Add(new LinkDto(selfLink, "self", "GET"));

            var updateLink = Url.Action(nameof(Put), "Motorcycle", new { id = motorcycleResponse.Id }, Request.Scheme);
            motorcycleResponse.Links.Add(new LinkDto(updateLink, "update_motorcycle", "PUT"));

            var deleteLink = Url.Action(nameof(Delete), "Motorcycle", new { id = motorcycleResponse.Id }, Request.Scheme);
            motorcycleResponse.Links.Add(new LinkDto(deleteLink, "delete_motorcycle", "DELETE"));

            return Ok(motorcycleResponse);
        }

        /// <summary>
        /// Cria uma nova moto no sistema.
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
        /// <param name="motorcycleRequest">Dados para a criação da nova moto.</param>
        /// <returns>O objeto da moto recém-criada, com seu ID e links HATEOAS.</returns>
        /// <response code="201">Retorna a moto recém-criada com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [ProducesResponseType(typeof(MotorcycleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<MotorcycleResponse>> Post([FromBody] MotorcycleRequest motorcycleRequest)
        {
            var motorcycle = _mapper.Map<Motorcycle>(motorcycleRequest);

            motorcycle.FlagAtivo = "S";
            motorcycle.CreatedAt = DateTime.UtcNow;

            _context.Motorcycles.Add(motorcycle);
            await _context.SaveChangesAsync();

            var motorcycleResponse = _mapper.Map<MotorcycleResponse>(motorcycle);

            var selfLink = Url.Action(nameof(GetById), "Motorcycle", new { id = motorcycleResponse.Id }, Request.Scheme);
            motorcycleResponse.Links.Add(new LinkDto(selfLink, "self", "GET"));

            var updateLink = Url.Action(nameof(Put), "Motorcycle", new { id = motorcycleResponse.Id }, Request.Scheme);
            motorcycleResponse.Links.Add(new LinkDto(updateLink, "update_motorcycle", "PUT"));

            var deleteLink = Url.Action(nameof(Delete), "Motorcycle", new { id = motorcycleResponse.Id }, Request.Scheme);
            motorcycleResponse.Links.Add(new LinkDto(deleteLink, "delete_motorcycle", "DELETE"));

            return CreatedAtAction(nameof(GetById), new { id = motorcycleResponse.Id }, motorcycleResponse);
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        /// <param name="id">O ID da moto a ser atualizada.</param>
        /// <param name="motorcycleRequest">Os novos dados para a moto.</param>
        /// <response code="204">Se a moto foi atualizada com sucesso.</response>
        /// <response code="404">Se a moto com o ID especificado não for encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] MotorcycleRequest motorcycleRequest)
        {
            var motorcycleFound = await _context.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycleFound == null || motorcycleFound.FlagAtivo == "N")
            {
                return NotFound();
            }

            _mapper.Map(motorcycleRequest, motorcycleFound);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma moto (soft delete).
        /// </summary>
        /// <param name="id">O ID da moto a ser excluída.</param>
        /// <response code="204">Se a moto foi excluída com sucesso.</response>
        /// <response code="404">Se a moto com o ID especificado não for encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var motorcycleFound = await _context.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycleFound == null)
            {
                return NotFound();
            }

            motorcycleFound.FlagAtivo = "N";

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}