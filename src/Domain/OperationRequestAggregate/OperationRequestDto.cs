using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.OperationRequestAggregate;

public class OperationRequestDto
{

    public string? RequestId { get; set; }

    public string? PatientID { get; set; }

    public string? OperationTypeID { get; set; }

    public string? DoctorID { get; set; }
    public string? Priority { get; set; }

    public string? Day { get; set; }

    public string? Month { get; set; }

    public string? Year { get; set; }
    public Dictionary<string, List<string>> specializationsStaff { get; set; }




    public OperationRequestDto()
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="operationRequestDto"/> class.
    /// </summary>
    public OperationRequestDto(OperationRequest operationRequest)
    {
        if (operationRequest == null)
        {
            throw new ArgumentNullException(nameof(operationRequest), "Operation Request cannot be null.");
        }

        RequestId = operationRequest.operationRequestID.ToString() ?? string.Empty;
        PatientID = operationRequest.patientID ?? string.Empty;
        OperationTypeID = operationRequest.operationTypeID ?? string.Empty;
        DoctorID = operationRequest.doctorID ?? string.Empty;

        Day = operationRequest.deadlineDate.Day().ToString() ?? string.Empty;
        Month = operationRequest.deadlineDate.Month().ToString() ?? string.Empty;
        Year = operationRequest.deadlineDate.Year().ToString() ?? string.Empty;

        Priority = operationRequest.priority.ToString() ?? string.Empty;

        specializationsStaff = operationRequest.specializations ?? new Dictionary<string, List<string>>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationRequestDto"/> class.
    /// </summary>
    /// <param name="RequestID"></param>
    /// <param name="PatientID"></param>
    /// <param name="OperationTypeID"></param>
    /// <param name="DoctorID"></param>
    /// <param name="Priority"></param>
    /// <param name="Day"></param>
    /// <param name="Month"></param>
    /// <param name="Year"></param>
    [JsonConstructor]
    public OperationRequestDto(string? requestId, string? patientID, string? operationTypeID, string? doctorID, string? priority, string? day, string? month, string? year)
    {
        RequestId = requestId;
        PatientID = patientID;
        OperationTypeID = operationTypeID;
        DoctorID = doctorID;
        Priority = priority;
        Day = day;
        Month = month;
        Year = year;
    }
}
