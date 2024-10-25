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
        public async Task CreateOperationType_ValidData_ReturnsOk()
        {
            // Arrange
            var operationTypeDto = new OperationTypeDto
            {
                OperationTypeName = "Knee Surgery",
                Specializations = new Dictionary<string, string>
                {
                    { "Orthopedics", "2" }
                },
                IsActive = true
            };

            _serviceMock.Setup(s => s.createOperationTypeAsync(It.IsAny<OperationTypeDto>(), "admin@example.com"))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateOperationType(operationTypeDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Operation type created successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task CreateOperationType_NullData_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.CreateOperationType(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Invalid operation type data. }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task CreateOperationType_InternalServerError_ReturnsInternalServerError()
        {
            // Arrange
            var operationTypeDto = new OperationTypeDto
            {
                OperationTypeName = "Knee Surgery",
                Specializations = new Dictionary<string, string>
                {
                    { "Orthopedics", "1" }
                },
                IsActive = true
            };

            _serviceMock.Setup(s => s.createOperationTypeAsync(It.IsAny<OperationTypeDto>(), "admin@example.com"))
                        .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.CreateOperationType(operationTypeDto);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("{ message = An error occurred: An error occurred }", internalServerErrorResult.Value.ToString());
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