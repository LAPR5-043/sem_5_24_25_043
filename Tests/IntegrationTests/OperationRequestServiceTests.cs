using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using src.Services;
using src.Services.IServices;
using Domain.OperationRequestAggregate;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using Domain.AppointmentAggregate;
using src.Domain.Shared;
using Microsoft.EntityFrameworkCore.Query;

namespace src.IntegrationTests
{
    public class OperationRequestServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOperationRequestRepository> _operationRequestRepositoryMock;
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;
        private readonly Mock<IPlanningModuleService> _planningModuleServiceMock;
        private readonly Mock<IStaffService> _staffServiceMock;
        private readonly Mock<ILogService> _logServiceMock;
        private readonly OperationRequestService _operationRequestService;
        private readonly Mock<IPatientService> _patientServiceMock;

        public OperationRequestServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _operationRequestRepositoryMock = new Mock<IOperationRequestRepository>();
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _planningModuleServiceMock = new Mock<IPlanningModuleService>();
            _staffServiceMock = new Mock<IStaffService>();
            _logServiceMock = new Mock<ILogService>();
            _patientServiceMock = new Mock<IPatientService>();

            _operationRequestService = new OperationRequestService(
                _unitOfWorkMock.Object,
                _operationRequestRepositoryMock.Object,
                _appointmentRepositoryMock.Object,
                _planningModuleServiceMock.Object,
                _staffServiceMock.Object,
                _patientServiceMock.Object,
                _logServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateOperationRequestAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = "123",
                DoctorID = "D123",
                OperationTypeID = "Heart Surgery",
                Day = "15",
                Month = "07",
                Year = "2023",
                Priority = "Emergency"
            };
            var email = "doctor@example.com";
            _staffServiceMock.Setup(service => service.GetIdFromEmailAsync(email)).ReturnsAsync("D123");

            // Act
            var result = await _operationRequestService.CreateOperationRequestAsync(operationRequestDto, email);

            // Assert
            Assert.True(result);
            _operationRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationRequest>()), Times.Once);
        }

        [Fact]
        public async Task CreateOperationRequestAsync_NullDto_ThrowsArgumentNullException()
        {
            // Arrange
            string email = "doctor@example.com";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _operationRequestService.CreateOperationRequestAsync(null, email));
            _operationRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationRequest>()), Times.Never);
        }

        [Theory]
        [InlineData(null, "15", "07", "2023", "The Patient's ID cannot be null or empty")]
        [InlineData("P123", "15", "07", "2023", "The Patient's ID must be a number")]
        [InlineData("123", "InvalidDay", "07", "2023", "Day must be a number")]
        [InlineData("123", "15", "InvalidMonth", "2023", "Month must be a number")]
        [InlineData("123", "15", "07", "InvalidYear", "Year must be a number")]
        public async Task CreateOperationRequestAsync_InvalidDateValues_ThrowsArgumentException(string patientID,
            string day, string month, string year, string expectedErrorMessage)
        {
            // Arrange
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = patientID,
                OperationTypeID = "Heart Surgery",
                Day = day,
                Month = month,
                Year = year,
                Priority = "Emergency"
            };
            var email = "doctor@example.com";
            _staffServiceMock.Setup(service => service.GetIdFromEmailAsync(email)).ReturnsAsync("D123");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _operationRequestService.CreateOperationRequestAsync(operationRequestDto, email));
            Assert.Contains(expectedErrorMessage, ex.Message);
            _operationRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationRequest>()), Times.Never);
        }

        [Fact]
        public async Task CreateOperationRequestAsync_DoctorNotFound_ThrowsException()
        {
            // Arrange
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = "123",
                OperationTypeID = "Heart Surgery",
                Day = "15",
                Month = "07",
                Year = "2023",
                Priority = "Emergency"
            };
            var email = "doctor@example.com";
            _staffServiceMock.Setup(service => service.GetIdFromEmailAsync(email)).ReturnsAsync((string)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _operationRequestService.CreateOperationRequestAsync(operationRequestDto, email));
            Assert.Contains("Doctor not found", ex.Message);
            _operationRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationRequest>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task GetOperationRequestFilteredAsync_ShouldReturnFilteredResults()
        {

            // Arrange
            var operationRequests = new List<OperationRequest>
            {
                new OperationRequest { operationRequestID = new OperationRequestID("1"), patientID = "1" , operationTypeID = "Heart Surgery", doctorID = "doc1", priority = Priority.Emergency, deadlineDate = new DeadlineDate(8, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("2"), patientID = "2" , operationTypeID = "Knee Surgery", doctorID = "doc2", priority = Priority.Urgent, deadlineDate = new DeadlineDate(14, 10, 2024) },
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetOperationRequestFilteredAsync("doc1", null, null, null, null, null, null);

            // Assert
            Assert.Single(result);
            Assert.Equal("doc1", result.First().DoctorID);
        }
        [Fact]
        public async Task GetOperationRequestFilteredAsync_ShouldReturnEmptyList_WhenNoMatches()
        {
            // Arrange
            var operationRequests = new List<OperationRequest>
            {
                new OperationRequest { operationRequestID = new OperationRequestID("1"), patientID = "1" , operationTypeID = "Heart Surgery", doctorID = "doc1", priority = Priority.Emergency, deadlineDate = new DeadlineDate(8, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("2"), patientID = "2" , operationTypeID = "Knee Surgery", doctorID = "doc2", priority = Priority.Urgent, deadlineDate = new DeadlineDate(14, 10, 2024) },
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetOperationRequestFilteredAsync("doc3", null, null, null, null, null, null);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetOperationRequestFilteredAsync_ShouldThrowArgumentException_WhenInvalidPriority()
        {
            // Arrange
            var operationRequests = new List<OperationRequest>
            {
                new OperationRequest { operationRequestID = new OperationRequestID("1"), patientID = "1" , operationTypeID = "Heart Surgery", doctorID = "doc1", priority = Priority.Emergency, deadlineDate = new DeadlineDate(8, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("2"), patientID = "2" , operationTypeID = "Knee Surgery", doctorID = "doc2", priority = Priority.Urgent, deadlineDate = new DeadlineDate(14, 10, 2024) },
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _operationRequestService.GetOperationRequestFilteredAsync(null, null, null, null, null, "invalidPriority", null));
        }


        [Fact]
        public async Task GetOperationRequestFilteredAsync_ShouldSortResults()
        {
            // Arrange
            var operationRequests = new List<OperationRequest>
            {
                new OperationRequest { operationRequestID = new OperationRequestID("1"), patientID = "1" , operationTypeID = "Heart Surgery", doctorID = "doc1", priority = Priority.Emergency, deadlineDate = new DeadlineDate(8, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("2"), patientID = "2" , operationTypeID = "Knee Surgery", doctorID = "doc2", priority = Priority.Urgent, deadlineDate = new DeadlineDate(14, 10, 2024) },
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetOperationRequestFilteredAsync(null, null, null, null, null, null, "patientID");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("1", result.First().PatientID);
        }

        [Fact]
        public async Task GetDoctorOperationRequestsAsync_ShouldReturnFilteredRequests()
        {
            // Arrange
            var doctorEmail = "doctor@example.com";
            var doctorId = "doc1";
            var operationRequests = new List<OperationRequest>
            {
                new OperationRequest { operationRequestID = new OperationRequestID("1"), patientID = "1" , operationTypeID = "Heart Surgery", doctorID = "doc1", priority = Priority.Emergency, deadlineDate = new DeadlineDate(8, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("2"), patientID = "2" , operationTypeID = "Knee Surgery", doctorID = "doc2", priority = Priority.Urgent, deadlineDate = new DeadlineDate(14, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("3"), patientID = "3" , operationTypeID = "Eye Surgery", doctorID = "doc1", priority = Priority.Urgent, deadlineDate = new DeadlineDate(19, 12, 2024) },
            };

            _staffServiceMock.Setup(s => s.GetIdFromEmailAsync(doctorEmail)).ReturnsAsync(doctorId);
            _operationRequestRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetDoctorOperationRequestsAsync(doctorEmail, null, null, null, null, null, null);

            // Assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task GetOperationRequestByPatientIdAsync_ShouldReturnRequestsForPatient()
        {
            // Arrange
            var patientId = "1";
            var operationRequests = new List<OperationRequest>
            {
                new OperationRequest { operationRequestID = new OperationRequestID("1"), patientID = "1" , operationTypeID = "Heart Surgery", doctorID = "doc1", priority = Priority.Emergency, deadlineDate = new DeadlineDate(8, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("2"), patientID = "1" , operationTypeID = "Knee Surgery", doctorID = "doc2", priority = Priority.Urgent, deadlineDate = new DeadlineDate(14, 10, 2024) },
                new OperationRequest { operationRequestID = new OperationRequestID("3"), patientID = "2" , operationTypeID = "Eye Surgery", doctorID = "doc1", priority = Priority.Urgent, deadlineDate = new DeadlineDate(19, 12, 2024) },
            };

            _operationRequestRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(operationRequests);

            // Act
            var result = await _operationRequestService.GetOperationRequestByPatientIdAsync(patientId);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateOperationRequestAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var email = "doctor@example.com";
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = "P123",
                DoctorID = "D123",
                OperationTypeID = "OT123",
                Day = "01",
                Month = "01",
                Year = "2023",
                Priority = "emergency"
            };

            var operationRequest = new OperationRequest
            {
                operationRequestID = new OperationRequestID(id.ToString()),
                patientID = "P123",
                doctorID = "D123",
                operationTypeID = "OT123",
                deadlineDate = new DeadlineDate(1, 1, 2023),
                priority = Priority.Emergency
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestID>()))
                                           .ReturnsAsync(operationRequest);

            // Act
            var result = await _operationRequestService.UpdateOperationRequestAsync(id, operationRequestDto, email);

            // Assert
            Assert.True(result);
            _operationRequestRepositoryMock.Verify(repo => repo.updateAsync(It.IsAny<OperationRequest>()), Times.Once);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.Is<string>(s => s.Contains("Update Operation Request")), email), Times.AtLeastOnce);
        }

        [Fact]
        public async Task UpdateOperationRequestAsync_OperationRequestNotFound_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            var email = "doctor@example.com";
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = "P123",
                DoctorID = "D123",
                OperationTypeID = "OT123",
                Day = "01",
                Month = "01",
                Year = "2023",
                Priority = "High"
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestID>()))
                                           .ReturnsAsync((OperationRequest)null);

            // Act
            var result = await _operationRequestService.UpdateOperationRequestAsync(id, operationRequestDto, email);

            // Assert
            Assert.False(result);
            _operationRequestRepositoryMock.Verify(repo => repo.updateAsync(It.IsAny<OperationRequest>()), Times.Never);
        }

        [Fact]
        public async Task UpdateOperationRequestAsync_InvalidData_ThrowsException()
        {
            // Arrange
            var id = 1;
            var email = "doctor@example.com";
            var operationRequestDto = new OperationRequestDto
            {
                PatientID = "P123",
                DoctorID = "D123",
                OperationTypeID = "OT123",
                Day = "invalid",
                Month = "invalid",
                Year = "invalid",
                Priority = "emergency"
            };

            var operationRequest = new OperationRequest
            {

                Id = new OperationRequestID("1"),
                operationRequestID = new OperationRequestID("1"),
                patientID = "1",
                doctorID = "D202400001",
                operationTypeID = "Knee Surgery",
                deadlineDate = new DeadlineDate(1, 1, 2025),
                priority = Priority.Emergency
            };

            _operationRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestID>()))
                                           .ReturnsAsync(operationRequest);

            var result = _operationRequestService.UpdateOperationRequestAsync(id, operationRequestDto, email);

            Assert.False(result.Result);
            _operationRequestRepositoryMock.Verify(repo => repo.updateAsync(It.IsAny<OperationRequest>()), Times.Never);
        }
    }
}