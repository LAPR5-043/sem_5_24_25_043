using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using src.Controllers;
using src.Services.IServices;
using Domain.PatientAggregate;

namespace src.IntegrationTests
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> mockPatientService;
        private readonly PatientController controller;


        public PatientControllerTests()
        {
            mockPatientService = new Mock<IPatientService>();
            controller = new PatientController(mockPatientService.Object);
        }

        [Fact]
        public async Task CreatePatient_ShouldReturnCreatedStatus()
        {
            // Arrange
            var patientDto = new PatientDto
            {
                MedicalRecordNumber = "P123",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhoneNumber = "+351987654321",
                DayOfBirth = "1",
                MonthOfBirth = "1",
                YearOfBirth = "2000",
                Gender = "Male"
            };

            mockPatientService.Setup(service => service.CreatePatientAsync(It.IsAny<PatientDto>()))
                              .ReturnsAsync(patientDto);

            // Act
            var result = await controller.CreatePatient(patientDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.GetPatientById), createdResult.ActionName);
            Assert.Equal(patientDto, createdResult.Value);
        }

        [Fact]
        public async Task CreatePatient_ShouldReturnBadRequestStatus()
        {
            // Act
            var result = await controller.CreatePatient(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Invalid patient data. }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task CreatePatient_ShouldReturnInternalServerError()
        {
            // Arrange
            var patientDto = new PatientDto
            {
                MedicalRecordNumber = "P123",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhoneNumber = "+351987654321",
                DayOfBirth = "1",
                MonthOfBirth = "1",
                YearOfBirth = "1990",
                Gender = "Male"
            };

            mockPatientService.Setup(service => service.CreatePatientAsync(It.IsAny<PatientDto>()))
                              .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.CreatePatient(patientDto);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("{ message = An error occurred while processing your request., error = An error occurred }", internalServerErrorResult.Value.ToString());
        }

        [Fact]
        public async Task DeletePatient_ShouldReturnOkStatus()
        {
            // Arrange
            mockPatientService.Setup(service => service.DeletePatientAsync(It.IsAny<string>(), It.IsAny<string>()))
                              .ReturnsAsync(true);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "admin@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.DeletePatient("P123456");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Patient deleted successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task DeletePatient_ShouldReturnNotFound()
        {
            // Arrange
            mockPatientService.Setup(service => service.DeletePatientAsync(It.IsAny<string>(), It.IsAny<string>()))
                              .ReturnsAsync(false);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "admin@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.DeletePatient("P123456");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Patient not found. }", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task UpdatePatient_ShouldReturnOkStatus()
        {
            // Arrange
            var Patient = new Patient
            {
                Id = new MedicalRecordNumber("2"),
                MedicalRecordNumber = new MedicalRecordNumber("2"),
                FirstName = new PatientFirstName("Jane"),
                LastName = new PatientLastName("Does"),
                FullName = new PatientFullName("Jane", "Does"),
                Email = new PatientEmail("Jane@email.com"),
                PhoneNumber = new PatientPhoneNumber("+351919991919"),
                EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                DateOfBirth = new DateOfBirth("01", "01", "1999"),
                Gender = Gender.Male,
                AllergiesAndConditions = new List<AllergiesAndConditions>(),
                AppointmentHistory = new AppointmentHistory()
            };
            var patientDto = new PatientDto(Patient);
            mockPatientService.Setup(service => service.UpdatePatientAsync(It.IsAny<string>(), It.IsAny<PatientDto>()))
                              .ReturnsAsync(true);

            mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<string>()))
                              .Returns(Task.FromResult(patientDto));


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "admin@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };


            // Act
            var result = await controller.UpdatePatient("P123456", patientDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Patient updated successfully. }", okResult.Value.ToString());
        }
        [Fact]
        public async Task UpdatePatient_ShouldReturnNotFoundStatus()
        {
            // Arrange
            var Patient = new Patient
            {
                Id = new MedicalRecordNumber("2"),
                MedicalRecordNumber = new MedicalRecordNumber("2"),
                FirstName = new PatientFirstName("Jane"),
                LastName = new PatientLastName("Does"),
                FullName = new PatientFullName("Jane", "Does"),
                Email = new PatientEmail("Jane@email.com"),
                PhoneNumber = new PatientPhoneNumber("+351919991919"),
                EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                DateOfBirth = new DateOfBirth("01", "01", "1999"),
                Gender = Gender.Male,
                AllergiesAndConditions = new List<AllergiesAndConditions>(),
                AppointmentHistory = new AppointmentHistory()
            };
            var patientDto = new PatientDto(Patient);
            mockPatientService.Setup(service => service.UpdatePatientAsync(It.IsAny<string>(), It.IsAny<PatientDto>()))
                              .ReturnsAsync(false);

            mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<string>()))
                              .Returns(Task.FromResult(patientDto));


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "admin@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };


            // Act
            var result = await controller.UpdatePatient("P123456", patientDto);

            // Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Patient not found. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task UpdatePatient_ShouldReturnBadRequestStatus()
        {
            // Arrange

            mockPatientService.Setup(service => service.UpdatePatientAsync(It.IsAny<string>(), It.IsAny<PatientDto>()))
                              .ReturnsAsync(false);

            mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<string>()))
                              .Returns(Task.FromResult<PatientDto>(null));

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "admin@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };


            // Act
            var result = await controller.UpdatePatient("P123456", null);

            // Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Invalid patient data. }", okResult.Value.ToString());
        }
        [Fact]
        public async Task AcceptPatientPendingRequests_ShouldReturnOkStatus()
        {
            // Arrange

            mockPatientService.Setup(service => service.AcceptRequests(It.IsAny<List<long>>()))
                              .ReturnsAsync(true);

            // Act
            var result = await controller.AcceptPatientPendingRequests("1,2");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = pending requests accepted successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task AcceptPatientPendingRequests_ShouldReturnNotFound()
        {
            // Arrange

            mockPatientService.Setup(service => service.AcceptRequests(It.IsAny<List<long>>()))
                              .ReturnsAsync(false);

            // Act
            var result = await controller.AcceptPatientPendingRequests("1,2");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = pending requests not found. }", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task AcceptPatientPendingRequests_ShouldReturnBadRequest()
        {
            // Act
            var result = await controller.AcceptPatientPendingRequests(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Invalid pending requests data. }", badRequestResult.Value.ToString());
        }

    }
}