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
        [Fact]
        public async Task DeletePatient_ShouldReturnOkStatus()
        {
            // Arrange
            var mockPatientService = new Mock<IPatientService>();

            mockPatientService.Setup(service => service.DeletePatientAsync(It.IsAny<string>(), It.IsAny<string>()))
                              .ReturnsAsync(true);

            var controller = new PatientController(mockPatientService.Object);

            
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
            var mockPatientService = new Mock<IPatientService>();

            mockPatientService.Setup(service => service.DeletePatientAsync(It.IsAny<string>(), It.IsAny<string>()))
                              .ReturnsAsync(false);

            var controller = new PatientController(mockPatientService.Object);

            
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
            var mockPatientService = new Mock<IPatientService>();

            var Patient =    new Patient
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

            var controller = new PatientController(mockPatientService.Object);



            
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
            var mockPatientService = new Mock<IPatientService>();

            var Patient =    new Patient
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

            var controller = new PatientController(mockPatientService.Object);



            
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
            var mockPatientService = new Mock<IPatientService>();

            
            mockPatientService.Setup(service => service.UpdatePatientAsync(It.IsAny<string>(), It.IsAny<PatientDto>()))
                              .ReturnsAsync(false);

            mockPatientService.Setup(service => service.GetPatientByIdAsync(It.IsAny<string>()))
                              .Returns(Task.FromResult<PatientDto>(null));

            var controller = new PatientController(mockPatientService.Object);



            
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
            var mockPatientService = new Mock<IPatientService>();

            mockPatientService.Setup(service => service.AcceptRequests(It.IsAny<List<long>>()))
                              .ReturnsAsync(true);

            var controller = new PatientController(mockPatientService.Object);

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
            var mockPatientService = new Mock<IPatientService>();

            mockPatientService.Setup(service => service.AcceptRequests(It.IsAny<List<long>>()))
                              .ReturnsAsync(false);

            var controller = new PatientController(mockPatientService.Object);


            // Act
            var result = await controller.AcceptPatientPendingRequests("1,2");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("{ message = pending requests not found. }", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task AcceptPatientPendingRequests_ShouldReturnBadRequest()
        {
            // Arrange
            var mockPatientService = new Mock<IPatientService>();

            var controller = new PatientController(mockPatientService.Object);

            // Act
            var result = await controller.AcceptPatientPendingRequests(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = Invalid pending requests data. }", badRequestResult.Value.ToString());
        }
            
    }
}