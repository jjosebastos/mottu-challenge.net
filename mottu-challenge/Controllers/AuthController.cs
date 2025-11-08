using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mottu_challenge.Dto.Request;
using mottu_challenge.Repository;
using mottu_challenge.Services;
using System.ComponentModel.DataAnnotations; // Usado para o DTO de resposta

namespace mottu_challenge.Controllers
{
    /// <summary>
    /// Controladora responsável pela autenticação de usuários e geração de tokens JWT.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Inicializa uma nova instância da <see cref="AuthController"/>.
        /// </summary>
        /// <param name="userRepository">O repositório para acesso aos dados do usuário.</param>
        /// <param name="tokenService">O serviço para geração de tokens JWT.</param>
        public AuthController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT.
        /// </summary>
        /// <remarks>
        /// Este endpoint valida as credenciais do usuário (username e senha). 
        /// Se as credenciais estiverem corretas e o usuário tiver um perfil de acesso,
        /// um token JWT é gerado e retornado.
        /// </remarks>
        /// <param name="model">O DTO (Objeto de Transferência de Dados) contendo o username e a senha.</param>
        /// <response code="200">OK. Retorna o token JWT de autenticação.</response>
        /// <response code="401">Unauthorized. Retornado se o username não existir, a senha estiver incorreta, ou o usuário não possuir um perfil (role) associado.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), 200)] // Sucesso
        [ProducesResponseType(typeof(string), 401)]         // Falha
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);

            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            if (!VerificarSenha(model.Password, user.Password))
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var roleName = await _userRepository.GetUserRoleAsync(user.IdUser);

            if (string.IsNullOrEmpty(roleName))
            {
                return Unauthorized("Usuário não possui perfil de acesso válido.");
            }

            // 6. Gera o token JWT
            var token = _tokenService.GenerateToken(user, roleName);

            // 7. Retorna o token em um DTO de Resposta
            return Ok(new LoginResponseDto { Token = token });
        }

        /// <summary>
        /// Verifica se a senha fornecida (em texto puro) corresponde ao hash salvo no banco.
        /// </summary>
        /// <param name="password">A senha em texto puro enviada pelo usuário.</param>
        /// <param name="passwordHash">O hash da senha (BCrypt) armazenado no banco de dados.</param>
        /// <returns><c>true</c> se a senha for válida; caso contrário, <c>false</c>.</returns>
        private bool VerificarSenha(string password, string passwordHash)
        {
            // Retorna true se a senha bate com o hash, false se não
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        /// <summary>
        /// DTO (Data Transfer Object) para a resposta do endpoint de login.
        /// </summary>
        public class LoginResponseDto
        {
            /// <summary>
            /// O token de autenticação JWT gerado.
            /// </summary>
            /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
            [Required]
            public string Token { get; set; }
        }
    }
}