using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Moq;
using Xunit;
using src.Services.Services;
using src.Services.IServices;
using Domain.PatientAggregate;
using src.Domain.Shared;
using sem_5_24_25_043;

namespace src.IntegrationTests
{
    public class PatientServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly Mock<ILogService> _logServiceMock;
        private readonly Mock<IPendingRequestService> _pendingRequestServiceMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<AuthService> _authServiceMock;
        private readonly Mock<ISensitiveDataService> _sensitiveDataServiceMock;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _logServiceMock = new Mock<ILogService>();
            _pendingRequestServiceMock = new Mock<IPendingRequestService>();
            _emailServiceMock = new Mock<IEmailService>();
            _authServiceMock = new Mock<AuthService>();
            _sensitiveDataServiceMock = new Mock<ISensitiveDataService>();

            _patientService = new PatientService(
                _unitOfWorkMock.Object,
                _patientRepositoryMock.Object,
                _logServiceMock.Object,
                _pendingRequestServiceMock.Object,
                _emailServiceMock.Object,
                _authServiceMock.Object
            );
        }

       /* [Fact]
        public async Task UpdatePatientAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var id = "P123456";
            var patientDto = new PatientDto
            {
                MedicalRecordNumber = id,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhoneNumber = "+0987654321",
                DayOfBirth = "01",
                MonthOfBirth = "01",
                YearOfBirth = "1990",
                Gender = "Male"
            };

            var patient = new Patient
            {
                MedicalRecordNumber = new MedicalRecordNumber(id),
                FirstName = new PatientFirstName("John"),
                LastName = new PatientLastName("Doe"),
                FullName = new PatientFullName("John", "Doe"),
                Email = new PatientEmail("john.doe@example.com"),
                PhoneNumber = new PatientPhoneNumber("+1234567890"),
                EmergencyContact = new EmergencyContact("Jane Doe", "+0987654321"),
                DateOfBirth = new DateOfBirth("01", "01", "1990"),
                Gender = Gender.Male
            };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<MedicalRecordNumber>()))
                                  .ReturnsAsync(patient);

            _sensitiveDataServiceMock.Setup(serv => serv.isSensitive(It.IsAny<string>()))
                                  .Returns(true);
            _sensitiveDataServiceMock.Setup(serv => serv.)
                                  .Returns(true);
                                  
            // Act
            var result = await _patientService.UpdatePatientAsync(id, patientDto);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }*/

        [Fact]
        public async Task UpdatePatientAsync_PatientNotFound_ThrowsException()
        {
            // Arrange
            var id = "P123456";
            var patientDto = new PatientDto
            {
                MedicalRecordNumber = id,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhoneNumber = "+0987654321",
                DayOfBirth = "01",
                MonthOfBirth = "01",
                YearOfBirth = "1990",
                Gender = "Male"
            };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<MedicalRecordNumber>()))
                                  .ReturnsAsync((Patient)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _patientService.UpdatePatientAsync(id, patientDto));
            Assert.Equal("Patient not found.", exception.Message);
        }

        [Fact]
        public async Task UpdatePatientAsync_NullData_ThrowsArgumentNullException()
        {
            // Arrange
            var id = "P123456";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _patientService.UpdatePatientAsync(id, null));
            Assert.Equal("Patient data is null. (Parameter 'patientDto')", exception.Message);
        }
    }
}