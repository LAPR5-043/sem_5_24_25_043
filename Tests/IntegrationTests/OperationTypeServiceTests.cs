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
    }
}