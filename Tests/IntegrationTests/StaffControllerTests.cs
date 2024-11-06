using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using src.Controllers;
using Domain.StaffAggregate;
using src.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using sem_5_24_25_043;
using src.Models;
using AppContext = src.Models.AppContext;
using src.Controllers.Services;
using src.Services.IServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src.IntegrationTests
{
   public class StaffControllerTests
    {

        [Fact]
        public async Task CreateStaff_ReturnsBadRequest_WhenStaffDtoIsNull()
        {
            Mock<IStaffService> mockStaffService = new Mock<IStaffService>();
            StaffController controller = new StaffController(mockStaffService.Object);

            // Arrange
            StaffDto staffDto = null;

            // Act
            var result = await controller.CreateStaff(staffDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("{ message = Invalid Staff data. }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task CreateStaff_ReturnsOk_WhenStaffIsCreatedSuccessfully()
        {
            Mock<IStaffService> mockStaffService = new Mock<IStaffService>();
            StaffController controller = new StaffController(mockStaffService.Object);

            // Arrange
            var staffDto = new StaffDto
            {
                StaffID = "0001",
                FirstName = "John",
                LastName = "Doe",
                Email = "doctor@example.com",
                PhoneNumber = "+351919911319",
                LicenseNumber = "123456",
                IsActive = true,
                SpecializationID = "Cardiology"
            };

            mockStaffService.Setup(service => service.CreateStaffAsync(staffDto)).ReturnsAsync(true);

            // Act
            var result = await controller.CreateStaff(staffDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("{ message = Staff member created successfully. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task CreateStaff_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            Mock<IStaffService> mockStaffService = new Mock<IStaffService>();
            StaffController controller = new StaffController(mockStaffService.Object);

            // Arrange
            var staffDto = new StaffDto();
            mockStaffService.Setup(service => service.CreateStaffAsync(staffDto)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.CreateStaff(staffDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred: Test exception", statusCodeResult.Value.GetType().GetProperty("message").GetValue(statusCodeResult.Value, null));
        }


        [Fact]
        public async Task CreateStaff_ReturnsStatusCode500_WhenStaffIsNotCreated()
        {
            Mock<IStaffService> mockStaffService = new Mock<IStaffService>();
            StaffController controller = new StaffController(mockStaffService.Object);

            // Arrange
            var staffDto = new StaffDto();
            mockStaffService.Setup(service => service.CreateStaffAsync(staffDto)).ReturnsAsync(false);

            // Act
            var result = await controller.CreateStaff(staffDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while creating the Staff member.", statusCodeResult.Value.GetType().GetProperty("message").GetValue(statusCodeResult.Value, null));
        }

        [Fact]
        public async Task GetStaffsFiltered_ShouldReturnOneStaff()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();
            var filteredStaff =

                new Staff
                {
                    Id = new StaffID("D202400001"),
                    staffID = new StaffID("D202400001"),
                    firstName = new StaffFirstName("John"),
                    lastName = new StaffLastName("Doe"),
                    fullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe")),
                    email = new StaffEmail("d123@doctor.com"),
                    phoneNumber = new StaffPhoneNumber("+351919919919"),
                    licenseNumber = new LicenseNumber("123456"),
                    isActive = true,
                    availabilitySlotsID = "D202400001",
                    specializationID = "Cardiology"
                }/*,
                new Staff
                {
                    Id = new StaffID("D202400011"),
                    staffID = new StaffID("D202400011"),
                    firstName = new StaffFirstName("Carlos"),
                    lastName = new StaffLastName("Moedas"),
                    fullName = new StaffFullName(new StaffFirstName("Carlos"), new StaffLastName("Moedas")),
                    email = new StaffEmail("D202400011@medopt.com"),
                    phoneNumber = new StaffPhoneNumber("+351919911319"),
                    licenseNumber = new LicenseNumber("121236"),
                    isActive = true,
                    availabilitySlots = new AvailabilitySlots(),
                    specializationID = "Neurology"
                }*/
        ;

            var filteredStaffs = new List<StaffDto>
            {
                new StaffDto(filteredStaff)
            };


            mockStaffService.Setup(service => service.getStaffsFilteredAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                    It.IsAny<string>(), It.IsAny<string>()));

            mockStaffService.SetReturnsDefault(Task.FromResult(filteredStaffs));



            var controller = new StaffController(mockStaffService.Object);

            // Act
            var result = await controller.GetStaffsFiltered("John", "", "", "", "");

            // Assert
            var okResult = Assert.IsType<ActionResult<List<StaffDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var staffList = Assert.IsType<List<StaffDto>>(returnValue.Value);
            Assert.Equal(1, staffList.Count);
            Assert.Equal("D202400001", staffList[0].StaffID);
            //Assert.Equal("Carlos Moedas", returnValue[1].fullName.id);
        }



        [Fact]
        public async Task GetStaffsFiltered_ShouldReturnTwoStaffsSortedByName()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();
            var filteredStaff1 = 
                new Staff
                {
                    Id = new StaffID("D202400011"),
                    staffID = new StaffID("D202400011"),
                    firstName = new StaffFirstName("Carlos"),
                    lastName = new StaffLastName("Moedas"),
                    fullName = new StaffFullName(new StaffFirstName("Carlos"), new StaffLastName("Moedas")),
                    email = new StaffEmail("D202400011@medopt.com"),
                    phoneNumber = new StaffPhoneNumber("+351919911319"),
                    licenseNumber = new LicenseNumber("121236"),
                    isActive = true,
                    availabilitySlotsID ="D202400011",
                    specializationID = "Neurology"
                };
            
            var filteredStaff2 =     
                new Staff
                {
                    Id = new StaffID("D202400001"),
                    staffID = new StaffID("D202400001"),
                    firstName = new StaffFirstName("John"),
                    lastName = new StaffLastName("Doe"),
                    fullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe")),
                    email = new StaffEmail("d123@doctor.com"),
                    phoneNumber = new StaffPhoneNumber("+351919919919"),
                    licenseNumber = new LicenseNumber("123456"),
                    isActive = true,
                    availabilitySlotsID = "D202400001",
                    specializationID = "Cardiology"
                };

        

            var filteredStaffs = new List<StaffDto>
            {
                new StaffDto(filteredStaff1),
                new StaffDto(filteredStaff2)
            };


            mockStaffService.Setup(service => service.getStaffsFilteredAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                                    It.IsAny<string>(), It.IsAny<string>()));
            
             mockStaffService.SetReturnsDefault(Task.FromResult(filteredStaffs));
                            


            var controller = new StaffController(mockStaffService.Object);

            // Act
            var result = await controller.GetStaffsFiltered( "","", "", "", "firstName");

            // Assert
            var okResult = Assert.IsType<ActionResult<List<StaffDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var staffList = Assert.IsType<List<StaffDto>>(returnValue.Value);
            Assert.Equal(2, staffList.Count);
            Assert.Equal("Carlos", staffList[0].FirstName);
            Assert.Equal("John", staffList[1].FirstName);
        }    


        [Fact]
        public async Task GetStaffsFiltered_ShouldReturnNoStaffsFilteredByName()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();
            

            var filteredStaffs = new List<StaffDto>();



            mockStaffService.Setup(service => service.getStaffsFilteredAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                    It.IsAny<string>(), It.IsAny<string>()));

            mockStaffService.SetReturnsDefault(Task.FromResult(filteredStaffs));



            var controller = new StaffController(mockStaffService.Object);

            // Act
            var result = await controller.GetStaffsFiltered("Jane", "", "", "", "");

            // Assert
            var okResult = Assert.IsType<ActionResult<List<StaffDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var staffList = Assert.IsType<List<StaffDto>>(returnValue.Value);
            Assert.Equal(0, staffList.Count);

        }

        [Fact]
        public async Task UpdateIsActive_ShouldReturnOkStatus()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();

            mockStaffService.Setup(service => service.UpdateIsActiveAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(true);

            var controller = new StaffController(mockStaffService.Object);

            // Mock the User.Claims
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "doctor@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.UpdateIsActive("D202400001");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("{ message = Staff deativated with success. }", okResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateIsActive_ShouldReturnNotFound()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();

            mockStaffService.Setup(service => service.UpdateIsActiveAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var controller = new StaffController(mockStaffService.Object);

            // Mock the User.Claims
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("custom:internalEmail", "doctor@example.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.UpdateIsActive("D202400001");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditStaff_ReturnsOk_WhenStaffIsUpdatedSuccessfully()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();
            mockStaffService.Setup(service =>
                    service.EditStaffAsync(It.IsAny<string>(), It.IsAny<StaffDto>(), It.IsAny<string>()))
                .ReturnsAsync(true);

        }

        [Fact]
        public async Task EditStaff_ReturnsBadRequest_WhenStaffDtoIsNull()
        {
            // Arrange
            var mockStaffService = new Mock<IStaffService>();
            var controller = new StaffController(mockStaffService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(
                            new Claim[] { new Claim("custom:internalEmail", "admin@example.com") }, "mock"))
                    }
                }
            };

            // Act
            var result = await controller.EditStaff("D202400001", null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Staff data is null.", badRequestResult.Value);
        }

    }
}