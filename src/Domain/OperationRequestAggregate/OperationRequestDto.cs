using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.OperationRequestAggregate;

public class OperationRequestDto
{

    public long? RequestId { get; set; }

    public int? patientID { get; set; }

    public string? operationType { get; set; }

    public string? doctorID { get; set; }
    public string? priority { get; set; }

    public int? day { get; set; }

    public int? month { get; set; }

    public int? year { get; set; }



    /// <summary>
    /// Initializes a new instance of the <see cref="operationRequestDto"/> class.
    /// </summary>
    public OperationRequestDto(OperationRequest operationRequest)
    {
        if (operationRequest == null)
        {
            throw new ArgumentNullException(nameof(operationRequest), "Operation Request cannot be null.");
        }
        /*
                RequestId = operationRequest.operationRequestID?.ID ?? string.Empty;

                operationType = operationRequest.operationTypeID.ToString() ?? string.Empty;
                doctorID = operationRequest.doctorID.ToString() ?? string.Empty;
                priority = operationRequest.priority.ToString() ?? string.Empty;

                day = operationRequest.deadlineDate?.Day() ?? string.Empty;
                month = operationRequest.deadlineDate?.Month() ?? string.Empty;
                year = operationRequest.deadlineDate?.Year() ?? string.Empty;
        */
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
    public OperationRequestDto(string MedicalRecordNumber, string FirstName, string LastName, string Email, string PhoneNumber, string EmergencyContactName,
    string EmergencyContactPhoneNumber, string DayOfBirth, string MonthOfBirth, string YearOfBirth, string Gender, List<string> AllergiesAndConditions, List<string> AppointmentHistory)
    {
        /*
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
        */
    }

}
