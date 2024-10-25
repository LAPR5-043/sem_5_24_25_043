using Moq;
using Xunit;
using src.Controllers;
using src.Services.IServices;
using Domain.OperationTypeAggregate;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
                new("custom:internalEmail", "admin@example.com"),
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

        /*[Fact]
        public async Task GetOperationTypes_ReturnsOk()
        {
            // Arrange
            var operationTypes = new List<OperationTypeDto>
            {
                new OperationTypeDto
                {
                    OperationTypeName = "Knee Surgery",
                    EstimatedDurationHours = "2",
                    EstimatedDurationMinutes = "30",
                    IsActive = true,
                    Specializations = new Dictionary<string, string>
                    {
                        { "Orthopedics", "2" }
                    }
                }
            };

            _serviceMock.Setup(s => s.getAllOperationTypesAsync())
                        .ReturnsAsync(operationTypes);

            // Act
            var result = await _controller.GetOperationTypes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<List<OperationTypeDto>>(okResult.Value);
            Assert.Equal(operationTypes.Count, returnValue.Count());
            Assert.Equal(operationTypes[0].OperationTypeName, returnValue.First().OperationTypeName);
        }

        [Fact]
        public async Task GetFilteredOperationTypes_ReturnsOk()
        {
            // Arrange
            var operationTypes = new List<OperationTypeDto>
            {
                new OperationTypeDto
                {
                    OperationTypeName = "Knee Surgery",
                    EstimatedDurationHours = "2",
                    EstimatedDurationMinutes = "30",
                    IsActive = true,
                    Specializations = new Dictionary<string, string>
                    {
                        { "Orthopedics", "2" }
                    }
                }
            };

            _serviceMock.Setup(s => s.getFilteredOperationTypesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(operationTypes);

            // Act
            var result = await _controller.GetFilteredOperationTypes("Knee Surgery", "Orthopedics", "true");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<List<OperationTypeDto>>(okResult.Value);
            Assert.Equal(operationTypes.Count, returnValue.Count());
            Assert.Equal(operationTypes[0].OperationTypeName, returnValue.First().OperationTypeName);
        }*/


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
