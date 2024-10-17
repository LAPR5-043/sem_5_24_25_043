using System;
using src.Domain.PatientAggregate;
using src.Domain.Shared;


namespace Domain.PatientAggregate
{
    /// <summary>
    /// Represents a patient entity in the domain.
    /// </summary>
    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot
    {
        /// <summary>
        /// Gets the medical record number of the patient.
        /// </summary>
        public MedicalRecordNumber medicalRecordNumber { get; private set; }

        /// <summary>
        /// Gets the first name of the patient.
        /// </summary>
        public PatientFirstName firstName { get; private set; }

        /// <summary>
        /// Gets the last name of the patient.
        /// </summary>
        public PatientLastName lastName { get; private set; }

        /// <summary>
        /// Gets the full name of the patient.
        /// </summary>
        public PatientFullName fullName { get; private set; }

        /// <summary>
        /// Gets the email of the patient.
        /// </summary>
        public PatientEmail email { get; private set; }

        /// <summary>
        /// Gets the phone number of the patient.
        /// </summary>
        public PatientPhoneNumber phoneNumber { get; private set; }

        /// <summary>
        /// Gets the emergency contact of the patient.
        /// </summary>
        public EmergencyContact emergencyContact { get; private set; }

        /// <summary>
        /// Gets the date of birth of the patient.
        /// </summary>
        public DateOfBirth dateOfBirth { get; private set; }

        /// <summary>
        /// Gets the gender of the patient.
        /// </summary>
        public Gender gender { get; private set; }

        /// <summary>
        /// Gets the list of allergies and conditions of the patient.
        /// </summary>
        public List<AllergiesAndConditions> allergiesAndConditions { get; private set; }

        /// <summary>
        /// Gets the appointment history of the patient.
        /// </summary>
        public AppointmentHistory appointmentHistory { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        public Patient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class with specified details.
        /// </summary>
        /// <param name="medicalRecordNumber">The medical record number of the patient.</param>
        /// <param name="firstName">The first name of the patient.</param>
        /// <param name="lastName">The last name of the patient.</param>
        /// <param name="email">The email of the patient.</param>
        /// <param name="phoneNumber">The phone number of the patient.</param>
        /// <param name="emergencyContactName">The name of the emergency contact.</param>
        /// <param name="emergencyContactPhoneNumber">The phone number of the emergency contact.</param>
        /// <param name="dayOfBirth">The day of birth of the patient.</param>
        /// <param name="monthOfBirth">The month of birth of the patient.</param>
        /// <param name="yearOfBirth">The year of birth of the patient.</param>
        /// <param name="gender">The gender of the patient.</param>
        public Patient(int medicalRecordNumber, string firstName, string lastName, string email, int phoneNumber, string emergencyContactName, int emergencyContactPhoneNumber, string dayOfBirth, string monthOfBirth, string yearOfBirth, string gender)
        {

            this.medicalRecordNumber = new MedicalRecordNumber(medicalRecordNumber);
            this.firstName = new PatientFirstName(firstName);
            this.lastName = new PatientLastName(lastName);
            this.fullName = new PatientFullName(firstName, lastName);
            this.email = new PatientEmail(email);
            this.phoneNumber = new PatientPhoneNumber(phoneNumber);
            this.emergencyContact = new EmergencyContact(emergencyContactName, emergencyContactPhoneNumber);
            this.dateOfBirth = new DateOfBirth(dayOfBirth, monthOfBirth, yearOfBirth);
            this.gender = GenderExtensions.FromString(gender);
            this.allergiesAndConditions = new List<AllergiesAndConditions>();
            this.appointmentHistory = new AppointmentHistory();
        }

        /// <summary>
        /// Adds an allergy or condition to the patient's record.
        /// </summary>
        /// <param name="allergyOrCondition">The allergy or condition to add.</param>
        public void AddAllergyOrCondition(string allergyOrCondition)
        {
            var condition = new AllergiesAndConditions(allergyOrCondition);
            allergiesAndConditions.Add(condition);
        }

        /// <summary>
        /// Removes an allergy or condition from the patient's record.
        /// </summary>
        /// <param name="allergyOrCondition">The allergy or condition to remove.</param>
        public void RemoveAllergyOrCondition(string allergyOrCondition)
        {
            var condition = allergiesAndConditions.FirstOrDefault(a => a.allergiesAndConditions == allergyOrCondition);
            if (condition != null)
            {
                allergiesAndConditions.Remove(condition);
            }
        }

        /// <summary>
        /// Adds an appointment to the patient's appointment history.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to add.</param>
        public void AddAppointment(int appointmentId)
        {
            appointmentHistory.AddAppointment(appointmentId);
        }

        /// <summary>
        /// Removes an appointment from the patient's appointment history.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to remove.</param>
        public void RemoveAppointment(int appointmentId)
        {
            appointmentHistory.RemoveAppointment(appointmentId);
        }

        /// <summary>
        /// Updates the emergency contact details of the patient.
        /// </summary>
        /// <param name="emergencyContactName">The name of the emergency contact.</param>
        /// <param name="emergencyContactPhoneNumber">The phone number of the emergency contact.</param>
        public void UpdateEmergencyContact(string emergencyContactName, int emergencyContactPhoneNumber)
        {

            emergencyContact = new EmergencyContact(emergencyContactName, emergencyContactPhoneNumber);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Patient)obj;
            return medicalRecordNumber.Equals(other.medicalRecordNumber);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return medicalRecordNumber.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"MedicalRecordNumber: {medicalRecordNumber},\n" +
                   $"FullName: {fullName},\n" +
                   $"Email: {email},\n" +
                   $"PhoneNumber: {phoneNumber},\n" +
                   $"EmergencyContact: {emergencyContact},\n" +
                   $"DateOfBirth: {dateOfBirth},\n" +
                   $"Gender: {gender},\n" +
                   $"AllergiesAndConditions: {string.Join(", ", allergiesAndConditions)},\n" +
                   $"AppointmentHistory: {appointmentHistory}";
        }
    }

}
