using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mottu_challenge.Dto.Request;
using mottu_challenge.Repository;
using mottu_challenge.Services;

namespace mottu_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> login([FromBody] UserLoginDto model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);


            if(user == null)
            {
                return Unauthorized();
            }

            if (!VerificarSenha(model.Password, user.Password))
            {
                return Unauthorized();
            }
            var roleName = await _userRepository.GetUserRoleAsync(user.IdUser);
        
            if (string.IsNullOrEmpty(roleName))
            {
              
                return Unauthorized("Usuário não possui perfil de acesso.");
            }
            var token = _tokenService.GenerateToken(user, roleName);

            return Ok(new
            {
                token  = token
            });
        }

        private bool VerificarSenha(string password, string passwordHash)
        {
             return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public class UserLoginAux()
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}
