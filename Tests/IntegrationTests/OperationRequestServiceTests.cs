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

        public OperationRequestServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _operationRequestRepositoryMock = new Mock<IOperationRequestRepository>();
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _planningModuleServiceMock = new Mock<IPlanningModuleService>();
            _staffServiceMock = new Mock<IStaffService>();
            _logServiceMock = new Mock<ILogService>();

            _operationRequestService = new OperationRequestService(
                _unitOfWorkMock.Object,
                _operationRequestRepositoryMock.Object,
                _appointmentRepositoryMock.Object,
                _planningModuleServiceMock.Object,
                _staffServiceMock.Object,
                _logServiceMock.Object
            );
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

            var result =  _operationRequestService.UpdateOperationRequestAsync(id, operationRequestDto, email);
            
            Assert.False(result.Result);
            _operationRequestRepositoryMock.Verify(repo => repo.updateAsync(It.IsAny<OperationRequest>()), Times.Never);
        }
    }
}