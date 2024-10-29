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
        public async Task GetPatientsFiltered_ReturnsOkResult_WithListOfPatients()
        {
            // Arrange
            var patients = new List<PatientDto>
            {
                new PatientDto { MedicalRecordNumber = "123", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" , PhoneNumber = "+1234567890", EmergencyContactName = "Jane Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "29", MonthOfBirth = "3", YearOfBirth = "2003", Gender = "Male"},
                new PatientDto { MedicalRecordNumber = "456", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "+1234567890", EmergencyContactName = "John Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "14", MonthOfBirth = "10", YearOfBirth = "2003", Gender = "Female"}
            };
            mockPatientService.Setup(service => service.GetPatientsFilteredAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(patients);

            // Act
            var result = await controller.GetPatientsFiltered(null, null, null, null, null, null, null, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<PatientDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetPatientsFiltered_ReturnsSpecificPatient_WithGivenName()
        {
            // Arrange
            var patients = new List<PatientDto>
            {
                new PatientDto { MedicalRecordNumber = "123", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" , PhoneNumber = "+1234567890", EmergencyContactName = "Jane Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "29", MonthOfBirth = "3", YearOfBirth = "2003", Gender = "Male"},
                new PatientDto { MedicalRecordNumber = "456", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "+1234567890", EmergencyContactName = "John Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "14", MonthOfBirth = "10", YearOfBirth = "2003", Gender = "Female"}
            };
            mockPatientService.Setup(service => service.GetPatientsFilteredAsync("John", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(new List<PatientDto> { patients[0] });

            // Act
            var result = await controller.GetPatientsFiltered("John", null, null, null, null, null, null, null);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<PatientDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var patientList = Assert.IsType<List<PatientDto>>(returnValue.Value);
            Assert.Equal(1, patientList.Count);
            Assert.Equal("123", patientList[0].MedicalRecordNumber);
        }


        [Fact]
        public async Task GetPatientsFiltered_ReturnsNotFound_WhenNoPatientsFound()
        {
            // Arrange
            var patients = new List<PatientDto>
            {
                new PatientDto { MedicalRecordNumber = "123", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" , PhoneNumber = "+1234567890", EmergencyContactName = "Jane Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "29", MonthOfBirth = "3", YearOfBirth = "2003", Gender = "Male"},
                new PatientDto { MedicalRecordNumber = "456", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "+1234567890", EmergencyContactName = "John Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "14", MonthOfBirth = "10", YearOfBirth = "2003", Gender = "Female"}
            };

            mockPatientService.Setup(service => service.GetPatientsFilteredAsync("Bob", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync((List<PatientDto>)null);

            // Act
            var result = await controller.GetPatientsFiltered("Bob", null, null, null, null, null, null, null);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetPatientById_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            var patients = new List<PatientDto>
            {
                new PatientDto { MedicalRecordNumber = "123", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" , PhoneNumber = "+1234567890", EmergencyContactName = "Jane Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "29", MonthOfBirth = "3", YearOfBirth = "2003", Gender = "Male"},
                new PatientDto { MedicalRecordNumber = "456", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "+1234567890", EmergencyContactName = "John Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "14", MonthOfBirth = "10", YearOfBirth = "2003", Gender = "Female"}
            };

            var patientId = "789";
            mockPatientService.Setup(service => service.GetPatientByIdAsync(patientId))
                        .ReturnsAsync((PatientDto)null);

            // Act
            var result = await controller.GetPatientById(patientId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Patient not found.", notFoundResult.Value.GetType().GetProperty("message").GetValue(notFoundResult.Value, null));
        }


        [Fact]
        public async Task GetPatientById_ReturnsOk_WhenPatientExists()
        {
            // Arrange
            var patients = new List<PatientDto>
            {
                new PatientDto { MedicalRecordNumber = "123", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" , PhoneNumber = "+1234567890", EmergencyContactName = "Jane Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "29", MonthOfBirth = "3", YearOfBirth = "2003", Gender = "Male"},
                new PatientDto { MedicalRecordNumber = "456", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "+1234567890", EmergencyContactName = "John Doe", EmergencyContactPhoneNumber = "+351987654321", DayOfBirth = "14", MonthOfBirth = "10", YearOfBirth = "2003", Gender = "Female"}
            };

            var patientId = "123";
            mockPatientService.Setup(service => service.GetPatientByIdAsync(patientId))
                        .ReturnsAsync(patients[0]);

            // Act
            var result = await controller.GetPatientById(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(patientId, okResult.Value.GetType().GetProperty("MedicalRecordNumber").GetValue(okResult.Value, null));
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

        [Fact]
        public async Task DeletePersonalAccount_ReturnsOk_WhenDeletionIsSuccessful()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("custom:internalEmail", "test@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Arrange
            mockPatientService.Setup(service => service.DeletePersonalAccountAsync(It.IsAny<string>(), It.IsAny<bool?>()))
                .ReturnsAsync(true);

            // Act
            var result = await controller.DeletePersonalAccount(true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Account deletion request successful }", okResult.Value.ToString());
        }

        [Fact]
        public async Task DeletePersonalAccount_ReturnsNotFound_WhenPatientNotFound()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
            new Claim("custom:internalEmail", "test@example.com")
                        }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Arrange
            mockPatientService.Setup(service => service.DeletePersonalAccountAsync(It.IsAny<string>(), It.IsAny<bool?>()))
                        .ReturnsAsync(false);

            // Act
            var result = await controller.DeletePersonalAccount(true);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = Patient personal account not found. }", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task DeletePersonalAccount_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                                    {
            new Claim("custom:internalEmail", "test@example.com")
                                    }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Arrange
            mockPatientService.Setup(service => service.DeletePersonalAccountAsync(It.IsAny<string>(), It.IsAny<bool?>()))
                        .ThrowsAsync(new System.Exception("Test exception"));

            // Act
            var result = await controller.DeletePersonalAccount(true);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("{ message = Test exception }", statusCodeResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteSensitiveData_ReturnsBadRequest_WhenPatientIdIsNull()
        {
            // Arrange
            string patientID = null;

            // Act
            var result = await controller.DeleteSensitiveData(patientID);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("{ message = Failed account deletion. }", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteSensitiveData_ReturnsOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            string patientID = "123";
            mockPatientService.Setup(service => service.DeleteSensitiveDataAsync(patientID)).ReturnsAsync(true);

            // Act
            var result = await controller.DeleteSensitiveData(patientID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("{ message = Patient account deletion confirmed. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteSensitiveData_ReturnsNotFound_WhenDeletionFails()
        {
            // Arrange
            string patientID = "123";
            mockPatientService.Setup(service => service.DeleteSensitiveDataAsync(patientID)).ReturnsAsync(false);

            // Act
            var result = await controller.DeleteSensitiveData(patientID);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("{ message = Failed account deletion. }", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task DeleteSensitiveData_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            // Arrange
            string patientID = "123";
            mockPatientService.Setup(service => service.DeleteSensitiveDataAsync(patientID)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.DeleteSensitiveData(patientID);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("{ message = An error occurred: Test exception }", statusCodeResult.Value.ToString());

        }
    }
}