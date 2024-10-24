using System;
using System.Linq;
using Xunit;
using Domain.PatientAggregate;
using src.Domain.Shared;



    public class PatientTests
    {
        [Fact]
        public void AddAllergyOrCondition_ShouldAddCondition()
        {
            // Arrange
            var patient = new Patient();
            var condition = "Peanut Allergy";

            // Act
            patient.AddAllergyOrCondition(condition);

            // Assert
            Assert.Contains(patient.AllergiesAndConditions, a => a.Value == condition);
        }

        [Fact]
        public void RemoveAllergyOrCondition_ShouldRemoveCondition()
        {
            // Arrange
            var patient = new Patient();
            var condition = "Peanut Allergy";
            patient.AddAllergyOrCondition(condition);

            // Act
            patient.RemoveAllergyOrCondition(condition);

            // Assert
            Assert.DoesNotContain(patient.AllergiesAndConditions, a => a.Value == condition);
        }

        [Fact]
        public void AddAppointment_ShouldAddAppointment()
        {
            // Arrange
            var patient = new Patient();
            var appointmentId = 123;

            // Act
            patient.AddAppointment(appointmentId);

            // Assert
            Assert.Contains(appointmentId, patient.AppointmentHistory.Appointments());
        }

        [Fact]
        public void RemoveAppointment_ShouldRemoveAppointment()
        {
            // Arrange
            var patient = new Patient();
            var appointmentId = 123;
            patient.AddAppointment(appointmentId);

            // Act
            patient.RemoveAppointment(appointmentId);

            // Assert
            Assert.DoesNotContain(appointmentId, patient.AppointmentHistory.Appointments());
        }

        [Fact]
        public void UpdateEmergencyContact_ShouldUpdateContact()
        {
            // Arrange
            var patient = new Patient();
            var contactName = "Jane Doe";
            var contactPhone = "+351919919919";

            // Act
            patient.UpdateEmergencyContact(contactName, contactPhone);

            // Assert
            Assert.Equal(contactName, patient.EmergencyContact.Name);
            Assert.Equal(contactPhone, patient.EmergencyContact.PhoneNumber);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameMedicalRecordNumber()
        {
            // Arrange
            var medicalRecordNumber = new MedicalRecordNumber("MRN123");
            var patient1 = new Patient { MedicalRecordNumber = medicalRecordNumber };
            var patient2 = new Patient { MedicalRecordNumber = medicalRecordNumber };

            // Act
            var result = patient1.Equals(patient2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentMedicalRecordNumber()
        {
            // Arrange
            var patient1 = new Patient { MedicalRecordNumber = new MedicalRecordNumber("MRN123") };
            var patient2 = new Patient { MedicalRecordNumber = new MedicalRecordNumber("MRN456") };

            // Act
            var result = patient1.Equals(patient2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCodeForSameMedicalRecordNumber()
        {
            // Arrange
            var medicalRecordNumber = new MedicalRecordNumber("MRN123");
            var patient1 = new Patient { MedicalRecordNumber = medicalRecordNumber };
            var patient2 = new Patient { MedicalRecordNumber = medicalRecordNumber };

            // Act
            var hashCode1 = patient1.GetHashCode();
            var hashCode2 = patient2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectStringRepresentation()
        {
            // Arrange
            var medicalRecordNumber = new MedicalRecordNumber("MRN123");
            var fullName = new PatientFullName("John", "Doe");
            var email = new PatientEmail("john.doe@example.com");
            var phoneNumber = new PatientPhoneNumber("+351919919919");
            var emergencyContact = new EmergencyContact("Jane Doe", "+351919919919");
            var dateOfBirth = new DateOfBirth("01", "01", "1990");
            var gender = Gender.Male;
            var patient = new Patient
            {
                MedicalRecordNumber = medicalRecordNumber,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmergencyContact = emergencyContact,
                DateOfBirth = dateOfBirth,
                Gender = gender
            };

            // Act
            var result = patient.ToString();

            // Assert
            var expected = $"MedicalRecordNumber: {medicalRecordNumber},\n" +
                           $"FullName: {fullName},\n" +
                           $"Email: {email},\n" +
                           $"PhoneNumber: {phoneNumber},\n" +
                           $"EmergencyContact: {emergencyContact},\n" +
                           $"DateOfBirth: {dateOfBirth},\n" +
                           $"Gender: {gender},\n" +
                           $"AllergiesAndConditions: ,\n" +
                           $"AppointmentHistory: {patient.AppointmentHistory}";
            Assert.Equal(expected, result);
        }
    }
