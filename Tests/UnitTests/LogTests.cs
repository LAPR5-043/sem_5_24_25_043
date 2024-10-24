using System;
using Xunit;
using src.Domain.Shared;

public class LogTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var action = "UserLogin";
        var timestamp = DateTime.Now;
        var email = "user@example.com";

        // Act
        var log = new Log(action, timestamp, email);

        // Assert
        Assert.Equal(action, log.action);
        Assert.Equal(timestamp, log.timestamp);
        Assert.Equal(email, log.email);
    }

    [Fact]
    public void DefaultConstructor_ShouldInitializePropertiesToDefaultValues()
    {
        // Act
        var log = new Log();

        // Assert
        Assert.Null(log.action);
        Assert.Equal(default(DateTime), log.timestamp);
        Assert.Null(log.email);
    }

    [Fact]
    public void SetLogId_ShouldUpdateLogId()
    {
        // Arrange
        var log = new Log();
        var logId = new LongId(1);

        // Act
        log.logId = logId;

        // Assert
        Assert.Equal(logId, log.logId);
    }

    [Fact]
    public void SetAction_ShouldUpdateAction()
    {
        // Arrange
        var log = new Log();
        var action = "UserLogout";

        // Act
        log.action = action;

        // Assert
        Assert.Equal(action, log.action);
    }

    [Fact]
    public void SetTimestamp_ShouldUpdateTimestamp()
    {
        // Arrange
        var log = new Log();
        var timestamp = DateTime.Now;

        // Act
        log.timestamp = timestamp;

        // Assert
        Assert.Equal(timestamp, log.timestamp);
    }

    [Fact]
    public void SetEmail_ShouldUpdateEmail()
    {
        // Arrange
        var log = new Log();
        var email = "user@example.com";

        // Act
        log.email = email;

        // Assert
        Assert.Equal(email, log.email);
    }
}