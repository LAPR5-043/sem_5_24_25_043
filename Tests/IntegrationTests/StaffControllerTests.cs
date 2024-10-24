using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using src.Controllers;
using Domain.StaffAggregate;
using src.Services.IServices;

namespace src.IntegrationTests
{
   public class StaffControllerTests
    {
        [Fact]
        public async Task GetStaffsFiltered_ShouldReturnFilteredStaffs()
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
                    availabilitySlots = new AvailabilitySlots(),
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
            var result = await controller.GetStaffsFiltered( "John","", "", "", "");

            // Assert
            var okResult = Assert.IsType<ActionResult<List<StaffDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            var staffList = Assert.IsType<List<StaffDto>>(returnValue.Value);
            Assert.Equal(1, staffList.Count);
            Assert.Equal("D202400001", staffList[0].StaffID);
            //Assert.Equal("Carlos Moedas", returnValue[1].fullName.id);
        }
    } 
}