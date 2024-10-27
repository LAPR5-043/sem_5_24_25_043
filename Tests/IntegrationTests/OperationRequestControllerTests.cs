using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using src.Controllers;
using src.Services.IServices;
using Domain.OperationRequestAggregate;

namespace src.Controllers.Tests
{
    public class OperationRequestControllerTests
    {
        private Mock<IOperationRequestService> _serviceMock;
        private OperationRequestController _controller;

        public OperationRequestControllerTests()
        {
            _serviceMock = new Mock<IOperationRequestService>();
            _controller = new OperationRequestController(_serviceMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "doctor@example.com"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task CreateOperationRequestAsync_ValidInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = "patientID",
                OperationTypeID = "Heart Surgery",
                Priority = "Emergency",
                Day = "12",
                Month = "10",
                Year = "2024"
            };

            _serviceMock.Setup(r => r.CreateOperationRequestAsync(It.IsAny<OperationRequestDto>(), "doctor@example.com"))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateOperationRequest(operationRequestDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.CreateOperationRequest), createdResult.ActionName);
            Assert.Equal(operationRequestDto, createdResult.Value);
        }


        [Fact]
        public async Task CreateOperationRequestAsync_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            OperationRequestDto operationRequestDto = null;

            // Act
            var result = await _controller.CreateOperationRequest(operationRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Operation request data is invalid. }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteOperationRequest_ConfirmedDeletion_ReturnsOk()
        {
            // Arrange
            var id = 1;
            _serviceMock.Setup(s => s.DeleteOperationRequestAsync(id, "doctor@example.com"))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteOperationRequest(id, true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Operation request deleted successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteOperationRequest_NotConfirmed_ReturnsBadRequest()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.DeleteOperationRequest(id, false);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Deletion not confirmed. }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteOperationRequest_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            _serviceMock.Setup(s => s.DeleteOperationRequestAsync(id, "doctor@example.com"))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteOperationRequest(id, true);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Operation request not found. }", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteOperationRequest_InternalServerError_ReturnsInternalServerError()
        {
            // Arrange
            var id = 1;
            _serviceMock.Setup(s => s.DeleteOperationRequestAsync(id, "doctor@example.com"))
                        .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.DeleteOperationRequest(id, true);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("{ message = An error occurred while deleting the operation request., error = An error occurred }", internalServerErrorResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateOperationRequest_ValidData_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var operationRequestDto = new OperationRequestDto();
            _serviceMock.Setup(s => s.UpdateOperationRequestAsync(id, operationRequestDto, "doctor@example.com"))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateOperationRequest(id, operationRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Operation request updated successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateOperationRequest_NullData_ReturnsBadRequest()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _controller.UpdateOperationRequest(id, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Invalid operation request data. }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateOperationRequest_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            var operationRequestDto = new OperationRequestDto();
            _serviceMock.Setup(s => s.UpdateOperationRequestAsync(id, operationRequestDto, "doctor@example.com"))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateOperationRequest(id, operationRequestDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Operation request not found. }", notFoundResult.Value.ToString());
        }


    }
}