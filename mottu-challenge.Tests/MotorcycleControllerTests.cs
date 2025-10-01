using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using mottu_challenge.Connection;
using mottu_challenge.Controllers;
using mottu_challenge.Dto.Response;
using mottu_challenge.Mappers;
using mottu_challenge.Model;
using Moq;
using Xunit;

namespace mottu_challenge.Tests
{
    public class MotorcycleControllerTests
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public MotorcycleControllerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _context.Motorcycles.Add(new Motorcycle { Id = 1, Model = "Moto Teste", Plate = "TST1234", Year = 2023, FlagAtivo = "S" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WhenMotorcycleExists_ShouldReturnOkWithMotorcycle()
        {
            var controller = new MotorcycleController(_context, _mapper);
            var existingId = 1;

            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock
                .Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("http://localhost/fake-url");

            controller.Url = urlHelperMock.Object;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var result = await controller.GetById(existingId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var motorcycleResponse = Assert.IsType<MotorcycleResponse>(okResult.Value);
            Assert.Equal(existingId, motorcycleResponse.Id);
        }
    }
}