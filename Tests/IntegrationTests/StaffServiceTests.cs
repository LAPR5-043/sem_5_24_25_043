using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using src.Services.Services;
using src.Services.IServices;
using Domain.StaffAggregate;
using src.Domain.Shared;
using src.Controllers.Services;
using sem_5_24_25_043;

namespace src.IntegrationTests
{
    public class StaffServiceTests
    {
        private readonly Mock<IStaffRepository> _staffRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogService> _logServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly StaffService _staffService;

        public StaffServiceTests()
        {
            _staffRepositoryMock = new Mock<IStaffRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _logServiceMock = new Mock<ILogService>();
            _authServiceMock = new Mock<IAuthService>();
            _emailServiceMock = new Mock<IEmailService>();

            _staffService = new StaffService(
                _unitOfWorkMock.Object,
                _staffRepositoryMock.Object,
                _logServiceMock.Object,
                _authServiceMock.Object,
                _emailServiceMock.Object
            );
        }

        [Fact]
        public async Task GetStaffsFilteredAsync_ValidFilters_ReturnsFilteredStaffs()
        {
            // Arrange
            var firstName = "";
            var lastName = "";
            var email = "";
            var specialization = "";
            var sortBy = "firstName";

            var staffList = new List<Staff>
            {
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
                },

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
                }
            };
            _staffRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(staffList);

            // Act
            var result = await _staffService.getStaffsFilteredAsync(firstName, lastName, email, specialization, sortBy);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Carlos", result[0].FirstName);
        }

        [Fact]
        public async Task GetStaffsFilteredAsync_ValidFilters_ReturnsFilteredStaffs2()
        {
            // Arrange
            var firstName = "Carlos";
            var lastName = "";
            var email = "";
            var specialization = "";
            var sortBy = "firstName";

            var staffList = new List<Staff>
            {
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
                },

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
                }
            };
            _staffRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(staffList);

            // Act
            var result = await _staffService.getStaffsFilteredAsync(firstName, lastName, email, specialization, sortBy);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Carlos", result[0].FirstName);
        }

        [Fact]
        public async Task GetStaffsFilteredAsync_NoFilters_ReturnsAllStaffs()
        {
            // Arrange
            var staffList = new List<Staff>
            {
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
                },

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
                }
            };

            _staffRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(staffList);

            // Act
            var result = await _staffService.getStaffsFilteredAsync(null, null, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetStaffsFilteredAsync_NoResults_ReturnsEmptyList()
        {
            // Arrange
            _staffRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(new List<Staff>());

            // Act
            var result = await _staffService.getStaffsFilteredAsync("", "", "", "", "");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateIsActiveAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var id = "D202400001";
            var adminEmail = "admin@example.com";
            var staff = new Staff
            {
                staffID = new StaffID(id),
                firstName = new StaffFirstName("John"),
                lastName = new StaffLastName("Doe"),
                email = new StaffEmail("john.doe@example.com"),
                specializationID = "Cardiology",
                isActive = true
            };

            _staffRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<StaffID>()))
                                .ReturnsAsync(staff);

            // Act
            var result = await _staffService.UpdateIsActiveAsync(id, adminEmail);

            // Assert
            Assert.True(result);
            _staffRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Staff>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.Is<string>(s => s.Contains("Staff status changed with success;StaffId:" + id)), adminEmail), Times.Once);
        }

        [Fact]
        public async Task UpdateIsActiveAsync_StaffNotFound_ReturnsFalse()
        {
            // Arrange
            var id = "D202400001";
            var adminEmail = "admin@example.com";

            _staffRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<StaffID>()))
                                .ReturnsAsync((Staff)null);

            // Act
            var result = await _staffService.UpdateIsActiveAsync(id, adminEmail);

            // Assert
            Assert.False(result);
            _staffRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Staff>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}