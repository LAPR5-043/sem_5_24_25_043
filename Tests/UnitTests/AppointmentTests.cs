using System;
using Xunit;
using src.Domain.AppointmentAggregate;
using src.Domain.Shared;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using Domain.AppointmentAggregate;


public class AppointmentTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var appointmentID = new AppointmentID("123");
        var requestID = 1;
        var roomID = 101;
        var dateAndTime = new DateAndTime(new DateTime(2023, 10, 1, 14, 30, 0));
        var status = StatusExtensions.FromString("Scheduled");

        // Act
        var appointment = new Appointment
        {
            appointmentID = appointmentID,
            requestID = requestID,
            roomID = roomID,
            dateAndTime = dateAndTime,
            status = status
        };

        // Assert
        Assert.Equal(appointmentID, appointment.appointmentID);
        Assert.Equal(requestID, appointment.requestID);
        Assert.Equal(roomID, appointment.roomID);
        Assert.Equal(dateAndTime, appointment.dateAndTime);
        Assert.Equal(status, appointment.status);
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateStatus()
    {
        // Arrange
        var appointment = new Appointment();
        var newStatus = "Completed";

        // Act
        appointment.ChangeStatus(newStatus);

        // Assert
        Assert.Equal(StatusExtensions.FromString(newStatus), appointment.status);
    }

    [Fact]
    public void ChangeDateAndTime_ShouldUpdateDateAndTime()
    {
        // Arrange
        var appointment = new Appointment();
        var newDay = 2;
        var newMonth = 10;
        var newYear = 2023;
        var newHour = 15;
        var newMinute = 45;

        // Act
        appointment.ChangeDateAndTime(newDay, newMonth, newYear, newHour, newMinute);

        // Assert
        Assert.Equal(new DateTime(newYear, newMonth, newDay, newHour, newMinute, 0), appointment.dateAndTime.Value);
    }

    [Fact]
    public void ChangeRoom_ShouldUpdateRoomID()
    {
        // Arrange
        var appointment = new Appointment();
        var newRoomID = 102;

        // Act
        appointment.ChangeRoom(newRoomID);

        // Assert
        Assert.Equal(newRoomID, appointment.roomID);
    }

    [Fact]
    public void Equals_ShouldReturnTrueForSameAppointmentID()
    {
        // Arrange
        var appointmentID = new AppointmentID("123");
        var appointment1 = new Appointment { appointmentID = appointmentID };
        var appointment2 = new Appointment { appointmentID = appointmentID };

        // Act
        var result = appointment1.Equals(appointment2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentAppointmentID()
    {
        // Arrange
        var appointment1 = new Appointment { appointmentID = new AppointmentID("123") };
        var appointment2 = new Appointment { appointmentID = new AppointmentID("124") };

        // Act
        var result = appointment1.Equals(appointment2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForSameAppointmentID()
    {
        // Arrange
        var appointmentID = new AppointmentID("123");
        var appointment1 = new Appointment { appointmentID = appointmentID };
        var appointment2 = new Appointment { appointmentID = appointmentID };

        // Act
        var hashCode1 = appointment1.GetHashCode();
        var hashCode2 = appointment2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentAppointmentID()
    {
        // Arrange
        var appointment1 = new Appointment { appointmentID = new AppointmentID("123") };
        var appointment2 = new Appointment { appointmentID = new AppointmentID("124") };

        // Act
        var hashCode1 = appointment1.GetHashCode();
        var hashCode2 = appointment2.GetHashCode();

        // Assert
        Assert.NotEqual(hashCode1, hashCode2);
    }
}