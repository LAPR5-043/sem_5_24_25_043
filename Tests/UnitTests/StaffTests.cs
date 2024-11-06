using System;
using Xunit;

public class StaffTests
{
    [Fact]
    public void ChangeFirstName_ShouldUpdateFirstName()
    {
        // Arrange
        var staff = new Staff();
        var newFirstName = "John";

        // Act
        staff.changeFirstName(newFirstName);

        // Assert
        Assert.Equal(newFirstName, staff.firstName.firstName);
    }

    [Fact]
    public void ChangeLastName_ShouldUpdateLastName()
    {
        // Arrange
        var staff = new Staff();
        var newLastName = "Doe";

        // Act
        staff.changeLastName(newLastName);

        // Assert
        Assert.Equal(newLastName, staff.lastName.lastName);
    }

    [Fact]
    public void ChangeEmail_ShouldUpdateEmail()
    {
        // Arrange
        var staff = new Staff();
        var newEmail = "john.doe@example.com";

        // Act
        staff.changeEmail(newEmail);

        // Assert
        Assert.Equal(newEmail, staff.email.email);
    }

    [Fact]
    public void ChangePhoneNumber_ShouldUpdatePhoneNumber()
    {
        // Arrange
        var staff = new Staff();
        var newPhoneNumber = "+351919919919";

        // Act
        staff.changePhoneNumber(newPhoneNumber);

        // Assert
        Assert.Equal(newPhoneNumber, staff.phoneNumber.phoneNumber);
    }

    [Fact]
    public void ChangeLicenseNumber_ShouldUpdateLicenseNumber()
    {
        // Arrange
        var staff = new Staff();
        var newLicenseNumber = "1204";

        // Act
        staff.changeLicenseNumber(newLicenseNumber);

        // Assert
        Assert.Equal(newLicenseNumber, staff.licenseNumber.licenseNumber);
    }

    [Fact]
    public void ChangeActiveStatus_ShouldUpdateIsActive()
    {
        // Arrange
        var staff = new Staff();
        var newStatus = true;

        // Act
        staff.changeActiveStatus(newStatus);

        // Assert
        Assert.Equal(newStatus, staff.isActive);
    }



    [Fact]
    public void ChangeFullName_ShouldUpdateFullName()
    {
        // Arrange
        var staff = new Staff();
        var newFullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe"));

        // Act
        staff.changeFullName(newFullName);

        // Assert
        Assert.Equal(newFullName, staff.fullName);
    }

    [Fact]
    public void ChangeSpecializationID_ShouldUpdateSpecializationID()
    {
        // Arrange
        var staff = new Staff();
        var newSpecializationID = "Orthopedics";

        // Act
        staff.changeSpecializationID(newSpecializationID);

        // Assert
        Assert.Equal(newSpecializationID, staff.specializationID);
    }

    [Fact]
    public void Equals_ShouldReturnTrueForSameStaffID()
    {
        // Arrange
        var staffID = new StaffID("D202400001");
        var staff1 = new Staff { staffID = staffID };
        var staff2 = new Staff { staffID = staffID };

        // Act
        var result = staff1.Equals(staff2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentStaffID()
    {
        // Arrange
        var staff1 = new Staff { staffID = new StaffID("D202400001") };
        var staff2 = new Staff { staffID = new StaffID("D202400022") };

        // Act
        var result = staff1.Equals(staff2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForSameStaffID()
    {
        // Arrange
        var staffID = new StaffID("D202400001");
        var staff1 = new Staff { staffID = staffID };
        var staff2 = new Staff { staffID = staffID };

        // Act
        var hashCode1 = staff1.GetHashCode();
        var hashCode2 = staff2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void ToString_ShouldReturnStaffIDAsString()
    {
        // Arrange
        var staffID = new StaffID("D202400001");
        var staff = new Staff { staffID = staffID };

        // Act
        var result = staff.ToString();

        // Assert
        Assert.Equal(staffID.ToString(), result);
    }
}