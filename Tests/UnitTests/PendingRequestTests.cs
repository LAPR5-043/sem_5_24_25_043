using System;
using Xunit;
using Xunit.Sdk;
using src.Domain.Shared;

public class PendingRequestTests
{
    [Fact]
    public void Equals_ShouldReturnTrueForSameRequestID()
    {
        // Arrange
        var requestId = new LongId(1);
        var pendingRequest1 = new PendingRequest { requestID = requestId };
        var pendingRequest2 = new PendingRequest { requestID = requestId };

        // Act
        var result = pendingRequest1.Equals(pendingRequest2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentRequestID()
    {
        // Arrange
        var pendingRequest1 = new PendingRequest { requestID = new LongId(1) };
        var pendingRequest2 = new PendingRequest { requestID = new LongId(2) };

        // Act
        var result = pendingRequest1.Equals(pendingRequest2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForSameRequestID()
    {
        // Arrange
        var requestId = new LongId(1);
        var pendingRequest1 = new PendingRequest { requestID = requestId };
        var pendingRequest2 = new PendingRequest { requestID = requestId };

        // Act
        var hashCode1 = pendingRequest1.GetHashCode();
        var hashCode2 = pendingRequest2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentRequestID()
    {
        // Arrange
        var pendingRequest1 = new PendingRequest { requestID = new LongId(1) };
        var pendingRequest2 = new PendingRequest { requestID = new LongId(2) };

        // Act
        var hashCode1 = pendingRequest1.GetHashCode();
        var hashCode2 = pendingRequest2.GetHashCode();

        // Assert
        Assert.NotEqual(hashCode1, hashCode2);
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var requestId = new LongId(1);
        var userId = "user123";
        var attributeName = "attribute";
        var pendingValue = "newValue";
        var oldValue = "oldValue";

        // Act
        var pendingRequest = new PendingRequest
        {
            requestID = requestId,
            userId = userId,
            attributeName = attributeName,
            pendingValue = pendingValue,
            oldValue = oldValue
        };

        // Assert
        Assert.Equal(requestId, pendingRequest.requestID);
        Assert.Equal(userId, pendingRequest.userId);
        Assert.Equal(attributeName, pendingRequest.attributeName);
        Assert.Equal(pendingValue, pendingRequest.pendingValue);
        Assert.Equal(oldValue, pendingRequest.oldValue);
    }
}