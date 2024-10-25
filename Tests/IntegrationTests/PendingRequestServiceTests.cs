using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using src.Services.Services;
using src.Services.IServices;
using src.Domain.Shared;

namespace src.IntegrationTests
{
    public class PendingRequestServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPendingRequestRepository> _pendingRequestRepositoryMock;
        private readonly Mock<ILogService> _logServiceMock;
        private readonly PendingRequestService _pendingRequestService;

        public PendingRequestServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _pendingRequestRepositoryMock = new Mock<IPendingRequestRepository>();
            _logServiceMock = new Mock<ILogService>();

            _pendingRequestService = new PendingRequestService(
                _unitOfWorkMock.Object,
                _pendingRequestRepositoryMock.Object,
                _logServiceMock.Object
            );
        }

        [Fact]
        public async Task AddPendingRequestAsync_ValidData_ReturnsPendingRequest()
        {
            // Arrange
            var userID = "U123";
            var oldValue = "oldValue";
            var newValue = "newValue";
            var type = "type";

            var pendingRequest = new PendingRequest
            {
                requestID = new LongId(1),
                userId = userID,
                oldValue = oldValue,
                pendingValue = newValue,
                attributeName = type
            };

            _pendingRequestRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<PendingRequest>()))
                                         .ReturnsAsync(pendingRequest);

            // Act
            var result = await _pendingRequestService.AddPendingRequestAsync(userID, oldValue, newValue, type);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userID, result.userId);
            Assert.Equal(oldValue, result.oldValue);
            Assert.Equal(newValue, result.pendingValue);
            Assert.Equal(type, result.attributeName);
            _pendingRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PendingRequest>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }


        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsPendingRequest()
        {
            // Arrange
            var id = new LongId(1);
            var pendingRequest = new PendingRequest
            {
                requestID = id,
                userId = "U123",
                oldValue = "oldValue",
                pendingValue = "newValue",
                attributeName = "type"
            };

            _pendingRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<LongId>()))
                                         .ReturnsAsync(pendingRequest);

            // Act
            var result =  _pendingRequestService.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.requestID);
            Assert.Equal("U123", result.userId);
            Assert.Equal("oldValue", result.oldValue);
            Assert.Equal("newValue", result.pendingValue);
            Assert.Equal("type", result.attributeName);
            _pendingRequestRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<LongId>()), Times.Once);
        }


        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var id = new LongId(1);

            _pendingRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<LongId>()))
                                         .ReturnsAsync((PendingRequest)null);

            // Act
            var result =  _pendingRequestService.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
            _pendingRequestRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<LongId>()), Times.Once);
        }
    }
}