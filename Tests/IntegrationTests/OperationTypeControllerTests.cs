using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using src.Controllers;
using src.Services.IServices;
using Domain.OperationTypeAggregate;

namespace src.Controllers.Tests
{
    public class OperationTypeControllerTests
    {
        private Mock<IOperationTypeService> _serviceMock;
        private OperationTypeController _controller;

        public OperationTypeControllerTests()
        {
            _serviceMock = new Mock<IOperationTypeService>();
            _controller = new OperationTypeController(_serviceMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "admin@example.com"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task DeactivateOperationType_ValidData_ReturnsOk()
        {
            // Arrange
            var id = "Knee Surgery";
            _serviceMock.Setup(s => s.deactivateOperationTypeAsync(id))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.DeactivateOperationType(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Operation type deactivated successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task DeactivateOperationType_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = "Knee Surgery";
            _serviceMock.Setup(s => s.deactivateOperationTypeAsync(id))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.DeactivateOperationType(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Operation type not found. }", notFoundResult.Value.ToString());
        }
    }
}