using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Represents a data transfer object for the Patient entity.
    /// </summary>
    public class PatientDto
    {
        /// <summary>
        /// Gets or sets the medical record number of the patient.
        /// </summary>
        public string? MedicalRecordNumber { get; set; }

        /// <summary>
        /// Gets or sets the first name of the patient.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the patient.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the patient.
        /// </summary>
        public string? FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets or sets the email of the patient.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the patient.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the emergency contact.
        /// </summary>
        public string? EmergencyContactName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the emergency contact.
        /// </summary>
        public string? EmergencyContactPhoneNumber { get; set; }

        public string? DayOfBirth { get; set; }

        public string? MonthOfBirth { get; set; }

        public string? YearOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the patient.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the list of allergies and conditions of the patient.
        /// </summary>
        public List<string>? AllergiesAndConditions { get; set; }

        /// <summary>
        /// Gets or sets the appointment history of the patient.
        /// </summary>
        public List<string>? AppointmentHistory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDto"/> class.
        /// </summary>
        public PatientDto(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");
            }

            MedicalRecordNumber = patient.MedicalRecordNumber?.medicalRecordNumber ?? string.Empty;
            FirstName = patient.FirstName?.ToString() ?? string.Empty;
            LastName = patient.LastName?.ToString() ?? string.Empty;
            Email = patient.Email?.ToString() ?? string.Empty;
            PhoneNumber = patient.PhoneNumber?.ToString() ?? string.Empty;

            EmergencyContactName = patient.EmergencyContact?.Name ?? string.Empty;
            EmergencyContactPhoneNumber = patient.EmergencyContact?.PhoneNumber ?? string.Empty;

            DayOfBirth = patient.DateOfBirth?.Day() ?? string.Empty;
            MonthOfBirth = patient.DateOfBirth?.Month() ?? string.Empty;
            YearOfBirth = patient.DateOfBirth?.Year() ?? string.Empty;

            Gender = patient.Gender.ToString();

            AllergiesAndConditions = patient.AllergiesAndConditions?.Select(a => a.ToString()).ToList() ?? new List<string>();
            AppointmentHistory = patient.AppointmentHistory?.Appointments().Select(a => a.ToString()).ToList() ?? new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDto"/> class.
        /// </summary>
        /// <param name="MedicalRecordNumber"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="EmergencyContactName"></param>
        /// <param name="EmergencyContactPhoneNumber"></param>
        /// <param name="DayOfBirth"></param>
        /// <param name="MonthOfBirth"></param>
        /// <param name="YearOfBirth"></param>
        /// <param name="Gender"></param>
        /// <param name="AllergiesAndConditions"></param>
        /// <param name="AppointmentHistory"></param>
        [JsonConstructor]
        public PatientDto(string MedicalRecordNumber, string FirstName, string LastName, string Email, string PhoneNumber, string EmergencyContactName,
        string EmergencyContactPhoneNumber, string DayOfBirth, string MonthOfBirth, string YearOfBirth, string Gender, List<string> AllergiesAndConditions, List<string> AppointmentHistory)
        {
            this.MedicalRecordNumber = MedicalRecordNumber;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.EmergencyContactName = EmergencyContactName;
            this.EmergencyContactPhoneNumber = EmergencyContactPhoneNumber;
            this.DayOfBirth = DayOfBirth;
            this.MonthOfBirth = MonthOfBirth;
            this.YearOfBirth = YearOfBirth;
            this.Gender = Gender;
            this.AllergiesAndConditions = AllergiesAndConditions;
            this.AppointmentHistory = AppointmentHistory;
        }
    }
}