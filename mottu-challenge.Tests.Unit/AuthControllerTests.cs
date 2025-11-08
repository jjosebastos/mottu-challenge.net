
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using mottu_challenge.Controllers;
using mottu_challenge.Dto.Request;
using mottu_challenge.Model;
using mottu_challenge.Repository;
using mottu_challenge.Service;
using mottu_challenge.Services;
using Xunit;

namespace mottu_challenge.Tests.Unit
{
    public class AuthControllerTests
    {

        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<ITokenService> _mockTokenService; // <-- DEVE SER A INTERFACE
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockTokenService = new Mock<ITokenService>(); 
            _authController = new AuthController(_mockRepo.Object, _mockTokenService.Object);
        }
        [Fact]
        public async Task Login_ComCredenciaisValidas_DeveRetornarOkComToken()
        {
            var loginDto = new UserLoginDto { Username = "teste", Password = "123" };

            var senhaHash = BCrypt.Net.BCrypt.HashPassword("123");

            var userDoBanco = new User { IdUser = 1, Username = "teste", Password = senhaHash };
            var tokenFalso = "token.jwt.falso";

 
            _mockRepo.Setup(repo => repo.GetByUsernameAsync("teste"))
                     .ReturnsAsync(userDoBanco);


            _mockRepo.Setup(repo => repo.GetUserRoleAsync(1))
                     .ReturnsAsync("Admin");

            _mockTokenService.Setup(service => service.GenerateToken(userDoBanco, "Admin"))
                             .Returns(tokenFalso);

            var resultado = await _authController.Login(loginDto);
            var okResult = Assert.IsType<OkObjectResult>(resultado);

            Assert.NotNull(okResult.Value);


            dynamic responseData = okResult.Value;
            Assert.Equal(tokenFalso, responseData.GetType().GetProperty("Token").GetValue(responseData, null));
        }
    }
}
