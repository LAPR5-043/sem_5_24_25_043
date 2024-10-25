using Microsoft.AspNetCore.Mvc;
using Moq;
using sem_5_24_25_043;
using src.Controllers;
using src.Services.IServices;
using System;
using System.Threading.Tasks;
using Xunit;

namespace src.IntegrationTests
{
    public class UserControllerTests
    {
        private readonly Mock<AuthService> mockAuthService;
        private readonly Mock<IPatientService> mockPatientService;
        private readonly UserController userController;

        public UserControllerTests()
        {
            mockAuthService = new Mock<AuthService>();
            mockPatientService = new Mock<IPatientService>();
            userController = new UserController(mockAuthService.Object, mockPatientService.Object);
        }

        [Fact]
        public async Task SignUpPatientAsync_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            string name = null;
            string phoneNumber = "+351123456789";
            string email = "test@example.com";
            string patientEmail = "patient@example.com";
            string password = "password";

            // Act
            var result = await userController.SignUpPatientAsync(name, phoneNumber, email, patientEmail, password);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = badRequestResult.Value;
            Assert.Equal("Invalid patient data.", response.GetType().GetProperty("message").GetValue(response, null));
        }

        [Fact]
        public async Task SignUpPatientAsync_SuccessfulSignup_ReturnsOk()
        {
            // Arrange
            string name = "John Doe";
            string phoneNumber = "+351123456789";
            string email = "test@example.com";
            string patientEmail = "patient@example.com";
            string password = "password";

            mockPatientService.Setup(service => service.SignUpNewPatientIamAsync(name, phoneNumber, email, patientEmail, password)).Returns(Task.CompletedTask);

            // Act
            var result = await userController.SignUpPatientAsync(name, phoneNumber, email, patientEmail, password);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = okResult.Value;
            Assert.Equal("Patient signed up successfully.", response.GetType().GetProperty("message").GetValue(response, null));
        }

        [Fact]
        public async Task SignUpPatientAsync_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            string name = "John Doe";
            string phoneNumber = "+351123456789";
            string email = "test@example.com";
            string patientEmail = "patient@example.com";
            string password = "password";

            mockPatientService.Setup(service => service.SignUpNewPatientIamAsync(name, phoneNumber, email, patientEmail, password)).Throws(new Exception("Test exception"));

            // Act
            var result = await userController.SignUpPatientAsync(name, phoneNumber, email, patientEmail, password);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            var response = statusCodeResult.Value;
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred: Test exception", response.GetType().GetProperty("message").GetValue(response, null));
        }
    }
}