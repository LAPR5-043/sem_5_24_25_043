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
        private readonly Mock<IAuthService> mockAuthService;
        private readonly Mock<IPatientService> mockPatientService;
        private readonly Mock<IStaffService> mockStaffService;
        private readonly UserController userController;

        public UserControllerTests()
        {
            mockAuthService = new Mock<IAuthService>();
            mockPatientService = new Mock<IPatientService>();
            mockStaffService = new Mock<IStaffService>();
            userController = new UserController(mockAuthService.Object, mockPatientService.Object, mockStaffService.Object);

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

        [Fact]
        public async Task LogInUserAsync_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            string email = null;
            string password = "password";

            // Act
            var result = await userController.LogInUserAsync(email, password);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = badRequestResult.Value;
            Assert.Equal("Invalid user data.", response.GetType().GetProperty("message").GetValue(response, null));
        }

        [Fact]
        public async Task LogInUserAsync_SuccessfulLogin_ReturnsOk()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            var tokens = (AccessToken: "access_token", IdToken: "id_token");

            mockAuthService.Setup(service => service.SignInAsync(email, password)).ReturnsAsync(tokens);

            // Act
            var result = await userController.LogInUserAsync(email, password);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = okResult.Value;
            Assert.Equal("access_token", response.GetType().GetProperty("accessToken").GetValue(response, null));
            Assert.Equal("id_token", response.GetType().GetProperty("idToken").GetValue(response, null));
        }

        [Fact]
        public async Task LogInUserAsync_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";

            mockAuthService.Setup(service => service.SignInAsync(email, password)).Throws(new Exception("Test exception"));

            // Act
            var result = await userController.LogInUserAsync(email, password);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            var response = statusCodeResult.Value;
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred: Test exception", response.GetType().GetProperty("message").GetValue(response, null));
        }

        [Fact]
        public async Task ResetPassword_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "test@example.com";
            mockAuthService.Setup(service => service.ResetPasswordAsync(email)).Returns(Task.CompletedTask);

            // Act
            var result = await userController.ResetPassword(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("{ message = Password reset email sent. }", okResult.Value.ToString());
        }


        [Fact]
        public async Task ResetPassword_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var email = "test@example.com";
            var exceptionMessage = "An error occurred.";
            mockAuthService.Setup(service => service.ResetPasswordAsync(email)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await userController.ResetPassword(email);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("{ Message = An error occurred. }", statusCodeResult.Value.ToString());

        }
    }
}