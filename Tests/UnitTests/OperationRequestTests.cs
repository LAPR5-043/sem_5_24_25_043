using System;
using Xunit;
using src.Domain.Shared;
using Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using sem_5_24_25_043.Domain.OperationRequestAggregate;

namespace src.Tests
{
    public class OperationRequestTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var operationRequestID = new OperationRequestID("1");
            var patientID = 123;
            var doctorID = "D202400001";

            // Act
            var operationRequest = new OperationRequest
            {
                operationRequestID = operationRequestID,
                patientID = patientID.ToString(),
                doctorID = doctorID
            };

            // Assert
            Assert.Equal(operationRequestID, operationRequest.operationRequestID);
            Assert.Equal(patientID.ToString(), operationRequest.patientID.ToString());
            Assert.Equal(doctorID, operationRequest.doctorID);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameOperationRequestID()
        {
            // Arrange
            var operationRequestID = new OperationRequestID("2");
            var operationRequest1 = new OperationRequest { operationRequestID = operationRequestID };
            var operationRequest2 = new OperationRequest { operationRequestID = operationRequestID };

            // Act
            var result = operationRequest1.Equals(operationRequest2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentOperationRequestID()
        {
            // Arrange
            var operationRequest1 = new OperationRequest { operationRequestID = new OperationRequestID("2") };
            var operationRequest2 = new OperationRequest { operationRequestID = new OperationRequestID("1") };

            // Act
            var result = operationRequest1.Equals(operationRequest2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCodeForSameOperationRequestID()
        {
            // Arrange
            var operationRequestID = new OperationRequestID("1");
            var operationRequest1 = new OperationRequest { operationRequestID = operationRequestID };
            var operationRequest2 = new OperationRequest { operationRequestID = operationRequestID };

            // Act
            var hashCode1 = operationRequest1.GetHashCode();
            var hashCode2 = operationRequest2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentOperationRequestID()
        {
            // Arrange
            var operationRequest1 = new OperationRequest { operationRequestID = new OperationRequestID("2") };
            var operationRequest2 = new OperationRequest { operationRequestID = new OperationRequestID("1") };

            // Act
            var hashCode1 = operationRequest1.GetHashCode();
            var hashCode2 = operationRequest2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}