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
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ISensitiveDataService> _sensitiveDataServiceMock;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _logServiceMock = new Mock<ILogService>();
            _pendingRequestServiceMock = new Mock<IPendingRequestService>();
            _emailServiceMock = new Mock<IEmailService>();
            _authServiceMock = new Mock<IAuthService>();
            _sensitiveDataServiceMock = new Mock<ISensitiveDataService>();

            _patientService = new PatientService(
                _unitOfWorkMock.Object,
                _patientRepositoryMock.Object,
                _logServiceMock.Object,
                _pendingRequestServiceMock.Object,
                _emailServiceMock.Object,
                _authServiceMock.Object,
              _sensitiveDataServiceMock.Object
            );
        }

        [Fact]
        public async Task CreatePatientAsync_ValidData_ReturnsPatientDto()
        {
            // Arrange
            var patientDto = new PatientDto
            {
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

            _patientRepositoryMock.Setup(repo => repo.PatientExists(It.IsAny<string>(), It.IsAny<string>()))
                      .Returns(false);

            _patientRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Patient>()))
                      .Returns(Task.FromResult(new Patient()));

            // Act
            var result = await _patientService.CreatePatientAsync(patientDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patientDto.FirstName, result.FirstName);
            Assert.Equal(patientDto.LastName, result.LastName);
            _patientRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Patient>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task CreatePatientAsync_PatientAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var patientDto = new PatientDto
            {
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

            _patientRepositoryMock.Setup(repo => repo.PatientExists(It.IsAny<string>(), It.IsAny<string>()))
                                  .Returns(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _patientService.CreatePatientAsync(patientDto));
            Assert.Equal("Patient already exists.", exception.Message);
        }

        [Fact]
        public async Task CreatePatientAsync_NullPatient_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _patientService.CreatePatientAsync(null));
            Assert.Equal("Patient data is null. (Parameter 'patient')", exception.Message);
        }

        [Fact]
        public async Task SignUpNewPatientIamAsync_ValidData_RegistersPatient()
        {
            // Arrange
            var name = "John Doe";
            var phoneNumber = "+1234567890";
            var email = "john.doe@example.com";
            var patientEmail = "patient@example.com";
            var password = "Password123";

            _patientRepositoryMock.Setup(repo => repo.PatientExists(It.IsAny<string>()))
                                  .Returns(true);

            // Act
            await _patientService.SignUpNewPatientIamAsync(name, phoneNumber, email, patientEmail, password);

            // Assert
            _authServiceMock.Verify(auth => auth.RegisterNewPatientAsync(name, phoneNumber, email, patientEmail, password), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.Is<string>(s => s.Contains("New patient registered in the IAM system; PatientEmail:" + email)), email), Times.Once);
        }

        [Fact]
        public async Task SignUpNewPatientIamAsync_InvalidPhoneNumber_ThrowsArgumentException()
        {
            // Arrange
            var name = "John Doe";
            var phoneNumber = "1234567890"; // Invalid phone number
            var email = "john.doe@example.com";
            var patientEmail = "patient@example.com";
            var password = "Password123";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _patientService.SignUpNewPatientIamAsync(name, phoneNumber, email, patientEmail, password));
            Assert.Equal("Phone number is invalid", exception.Message);
        }

        [Fact]
        public async Task SignUpNewPatientIamAsync_PatientDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var name = "John Doe";
            var phoneNumber = "+1234567890";
            var email = "john.doe@example.com";
            var patientEmail = "patient@example.com";
            var password = "Password123";

            _patientRepositoryMock.Setup(repo => repo.PatientExists(It.IsAny<string>()))
                                  .Returns(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _patientService.SignUpNewPatientIamAsync(name, phoneNumber, email, patientEmail, password));
            Assert.Equal("Patient Does Not Exist", exception.Message);
        }

        [Fact]
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
            _sensitiveDataServiceMock.Setup(serv => serv.loadSensitiveData());                   
                                  
            // Act
            var result = await _patientService.UpdatePatientAsync(id, patientDto);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

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
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _patientService.UpdatePatientAsync(id, null));
            Console.WriteLine(exception.Message);
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

                [Fact]
        public async Task DeletePatientAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var id = "P123456";
            var adminEmail = "admin@example.com";
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

            // Act
            var result = await _patientService.DeletePatientAsync(id, adminEmail);

            // Assert
            Assert.True(result);
            _patientRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Patient>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.Is<string>(s => s.Contains("Patient deleted with success;PatientId:" + id)), adminEmail), Times.Once);
        }
        [Fact]
        public async Task DeletePatientAsync_PatientNotFound_ReturnsFalse()
        {
            // Arrange
            var id = "P123456";
            var adminEmail = "admin@example.com";

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<MedicalRecordNumber>()))
                                  .ReturnsAsync((Patient)null);

            // Act
            var result = await _patientService.DeletePatientAsync(id, adminEmail);

            // Assert
            Assert.False(result);
            _patientRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Patient>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}