using System;
using System.Collections.Generic;
using Xunit;
using src.Domain.Shared;

public class OperationTypeTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var operationTypeName = "Surgery";
        var anesthesia = 2;
        var operation = 30;
        var cleaning = 0;
        var isActive = true;
        var specializations = new Dictionary<string, int> { { "Cardiology", 5 } };

        // Act
        var operationType = new OperationType(operationTypeName, anesthesia, operation, cleaning, isActive, specializations);
        // Assert
        Assert.Equal(operationTypeName, operationType.operationTypeName.operationTypeName);
        Assert.Equal(anesthesia, operationType.estimatedDuration.anesthesia);
        Assert.Equal(operation, operationType.estimatedDuration.operation);
        Assert.Equal(cleaning, operationType.estimatedDuration.cleaning);
        Assert.Equal(isActive, operationType.isActive);
        Assert.Equal(specializations, operationType.specializations);
    }

    [Fact]
    public void ChangeOperationTypeName_ShouldUpdateOperationTypeName()
    {
        // Arrange
        var operationType = new OperationType();
        var newOperationTypeName = "New Surgery";

        // Act
        operationType.changeOperationTypeName(newOperationTypeName);

        // Assert
        Assert.Equal(newOperationTypeName, operationType.operationTypeName.operationTypeName);
    }

    [Fact]
    public void ChangeEstimatedDuration_ShouldUpdateEstimatedDuration()
    {
        // Arrange
        var operationType = new OperationType();
        var anesthesia = 2;
        var operation = 30;
        var cleaning = 0;

        // Act
        operationType.changeEstimatedDuration(anesthesia, operation, cleaning);

        // Assert
        Assert.Equal(anesthesia, operationType.estimatedDuration.anesthesia);
        Assert.Equal(operation, operationType.estimatedDuration.operation);
        Assert.Equal(cleaning, operationType.estimatedDuration.cleaning);
    }

    [Fact]
    public void ChangeActiveStatus_ShouldUpdateIsActive()
    {
        // Arrange
        var operationType = new OperationType();
        var newStatus = true;

        // Act
        operationType.changeActiveStatus(newStatus);

        // Assert
        Assert.Equal(newStatus, operationType.isActive);
    }

    [Fact]
    public void ChangeStatus_ShouldToggleIsActive()
    {
        // Arrange
        var operationType = new OperationType();
        var initialStatus = operationType.isActive;

        // Act
        operationType.changeStatus();

        // Assert
        Assert.Equal(!initialStatus, operationType.isActive);
    }

    [Fact]
    public void Equals_ShouldReturnTrueForSameOperationTypeNameAndEstimatedDuration()
    {
        // Arrange
        var operationTypeName = new OperationTypeName("Surgery");
        var estimatedDuration = new EstimatedDuration(2, 30, 23);
        var operationType1 = new OperationType(operationTypeName, estimatedDuration, true, new Dictionary<string, int>());
        var operationType2 = new OperationType(operationTypeName, estimatedDuration, true, new Dictionary<string, int>());

        // Act
        var result = operationType1.Equals(operationType2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentOperationTypeNameOrEstimatedDuration()
    {
        // Arrange
        var operationTypeName1 = new OperationTypeName("Surgery");
        var operationTypeName2 = new OperationTypeName("Consultation");
        var estimatedDuration1 = new EstimatedDuration(2, 30,1);
        var estimatedDuration2 = new EstimatedDuration(1, 2,1);
        var operationType1 = new OperationType(operationTypeName1, estimatedDuration1, true, new Dictionary<string, int>());
        var operationType2 = new OperationType(operationTypeName2, estimatedDuration1, true, new Dictionary<string, int>());
        var operationType3 = new OperationType(operationTypeName1, estimatedDuration2, true, new Dictionary<string, int>());

        // Act
        var result1 = operationType1.Equals(operationType2);
        var result2 = operationType1.Equals(operationType3);

        // Assert
        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForSameOperationTypeName()
    {
        // Arrange
        var operationTypeName = new OperationTypeName("Surgery");
        var operationType1 = new OperationType(operationTypeName, new EstimatedDuration(2, 30,1), true, new Dictionary<string, int>());
        var operationType2 = new OperationType(operationTypeName, new EstimatedDuration(2, 30,1), true, new Dictionary<string, int>());

        // Act
        var hashCode1 = operationType1.GetHashCode();
        var hashCode2 = operationType2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void ToString_ShouldReturnOperationTypeName()
    {
        // Arrange
        var operationTypeName = new OperationTypeName("Surgery");
        var operationType = new OperationType(operationTypeName, new EstimatedDuration(2, 30,1), true, new Dictionary<string, int>());

        // Act
        var result = operationType.ToString();

        // Assert
        Assert.Equal(operationTypeName.operationTypeName, result);
    }
}