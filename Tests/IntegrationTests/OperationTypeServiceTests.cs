using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using src.Controllers.Services;
using src.Services.IServices;
using Domain.OperationTypeAggregate;
using src.Domain.Shared;

namespace src.IntegrationTests
{
    public class OperationTypeServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOperationTypeRepository> _operationTypeRepositoryMock;
        private readonly Mock<ILogService> _logServiceMock;
        private readonly OperationTypeService _operationTypeService;

        public OperationTypeServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _operationTypeRepositoryMock = new Mock<IOperationTypeRepository>();
            _logServiceMock = new Mock<ILogService>();

            _operationTypeService = new OperationTypeService(
                _unitOfWorkMock.Object,
                _operationTypeRepositoryMock.Object,
                _logServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateOperationTypeAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var operationTypeDto = new OperationTypeDto
            {
                OperationTypeName = "OT123",
                EstimatedDurationHours = "1",
                EstimatedDurationMinutes = "30",
                IsActive = true,
                Specializations = new Dictionary<string, string> { { "Cardiology", "1" } }
            };
            var adminEmail = "admin@example.com";

            _operationTypeRepositoryMock.Setup(repo => repo.OperationTypeExists(It.IsAny<string>()))
                                        .Returns(false);

            // Act
            var result = await _operationTypeService.createOperationTypeAsync(operationTypeDto, adminEmail);

            // Assert
            Assert.True(result);
            _operationTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationType>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.IsAny<string>(), adminEmail), Times.Once);
        }

        [Fact]
        public async Task CreateOperationTypeAsync_OperationTypeAlreadyExists_ThrowsException()
        {
            // Arrange
            var operationTypeDto = new OperationTypeDto
            {
                OperationTypeName = "OT123",
                EstimatedDurationHours = "1",
                EstimatedDurationMinutes = "30",
                IsActive = true,
                Specializations = new Dictionary<string, string> { { "Cardiology", "1" } }
            };
            var adminEmail = "admin@example.com";

            _operationTypeRepositoryMock.Setup(repo => repo.OperationTypeExists(It.IsAny<string>()))
                                        .Returns(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _operationTypeService.createOperationTypeAsync(operationTypeDto, adminEmail));
            _operationTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationType>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.IsAny<string>(), adminEmail), Times.Never);
        }

        [Fact]
        public async Task CreateOperationTypeAsync_NullOperationType_ThrowsArgumentNullException()
        {
            // Arrange
            OperationTypeDto operationTypeDto = null;
            var adminEmail = "admin@example.com";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _operationTypeService.createOperationTypeAsync(operationTypeDto, adminEmail));
            _operationTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<OperationType>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
            _logServiceMock.Verify(log => log.CreateLogAsync(It.IsAny<string>(), adminEmail), Times.Never);
        }

        [Fact]
        public async Task DeactivateOperationTypeAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var id = "OT123";
            var operationType = new OperationType
            {
                operationTypeName = new OperationTypeName(id),
                estimatedDuration = new EstimatedDuration(1, 30),
                isActive = true,
                specializations = new Dictionary<string, int> { { "Cardiology", 1 } }
            };

            _operationTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationTypeName>()))
                                        .ReturnsAsync(operationType);

            // Act
            var result = await _operationTypeService.deactivateOperationTypeAsync(id);

            // Assert
            Assert.True(result);
            _operationTypeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<OperationType>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeactivateOperationTypeAsync_OperationTypeNotFound_ReturnsFalse()
        {
            // Arrange
            var id = "OT123";

            _operationTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationTypeName>()))
                                        .ReturnsAsync((OperationType)null);

            // Act
            var result = await _operationTypeService.deactivateOperationTypeAsync(id);

            // Assert
            Assert.False(result);
            _operationTypeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<OperationType>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAllOperationTypesAsync_ReturnsAllOperationTypes()
        {
            // Arrange
            var operationTypes = new List<OperationType>
            {
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT123"),
                    estimatedDuration = new EstimatedDuration(1, 30),
                    isActive = true,
                    specializations = new Dictionary<string, int> { { "Cardiology", 1 } }
                },
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT124"),
                    estimatedDuration = new EstimatedDuration(2, 0),
                    isActive = false,
                    specializations = new Dictionary<string, int> { { "Neurology", 2 } }
                }
            };

            _operationTypeRepositoryMock.Setup(repo => repo.GetAllAsync())
                                        .ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.getAllOperationTypesAsync();

            // Assert
            Assert.Equal(2, result.Value.Count());
            Assert.Contains(result.Value, ot => ot.OperationTypeName == "OT123");
            Assert.Contains(result.Value, ot => ot.OperationTypeName == "OT124");
        }

        [Fact]
        public async Task GetFilteredOperationTypesAsync_WithNameFilter_ReturnsFilteredOperationTypes()
        {
            // Arrange
            var operationTypes = new List<OperationType>
            {
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT123"),
                    estimatedDuration = new EstimatedDuration(1, 30),
                    isActive = true,
                    specializations = new Dictionary<string, int> { { "Cardiology", 1 } }
                },
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT124"),
                    estimatedDuration = new EstimatedDuration(2, 0),
                    isActive = false,
                    specializations = new Dictionary<string, int> { { "Neurology", 2 } }
                }
            };

            _operationTypeRepositoryMock.Setup(repo => repo.GetAllAsync())
                                        .ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.getFilteredOperationTypesAsync("OT123", null, null);

            // Assert
            Assert.Single(result.Value);
            Assert.Contains(result.Value, ot => ot.OperationTypeName == "OT123");
        }

        [Fact]
        public async Task GetFilteredOperationTypesAsync_WithSpecializationFilter_ReturnsFilteredOperationTypes()
        {
            // Arrange
            var operationTypes = new List<OperationType>
            {
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT123"),
                    estimatedDuration = new EstimatedDuration(1, 30),
                    isActive = true,
                    specializations = new Dictionary<string, int> { { "Cardiology", 1 } }
                },
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT124"),
                    estimatedDuration = new EstimatedDuration(2, 0),
                    isActive = false,
                    specializations = new Dictionary<string, int> { { "Neurology", 2 } }
                }
            };

            _operationTypeRepositoryMock.Setup(repo => repo.GetAllAsync())
                                        .ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.getFilteredOperationTypesAsync(null, "Cardiology", null);

            // Assert
            Assert.Single(result.Value);
            Assert.Contains(result.Value, ot => ot.Specializations.ContainsKey("Cardiology"));
        }

        [Fact]
        public async Task GetFilteredOperationTypesAsync_WithStatusFilter_ReturnsFilteredOperationTypes()
        {
            // Arrange
            var operationTypes = new List<OperationType>
            {
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT123"),
                    estimatedDuration = new EstimatedDuration(1, 30),
                    isActive = true,
                    specializations = new Dictionary<string, int> { { "Cardiology", 1 } }
                },
                new OperationType
                {
                    operationTypeName = new OperationTypeName("OT124"),
                    estimatedDuration = new EstimatedDuration(2, 0),
                    isActive = false,
                    specializations = new Dictionary<string, int> { { "Neurology", 2 } }
                }
            };

            _operationTypeRepositoryMock.Setup(repo => repo.GetAllAsync())
                                        .ReturnsAsync(operationTypes);

            // Act
            var result = await _operationTypeService.getFilteredOperationTypesAsync(null, null, "true");

            // Assert
            Assert.Single(result.Value);
            Assert.Contains(result.Value, ot => ot.IsActive == true);
        }
    }
}