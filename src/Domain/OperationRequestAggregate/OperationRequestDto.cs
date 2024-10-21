using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.OperationRequestAggregate;

public class OperationRequestDto
{

    public string OperationRequestID { get; set; }

    public string PatientID { get; set; }

    public string DoctorID { get; set; }

    public string OperationTypeID { get; set; }

    public string DeadlineDate { get; set; }

    public string Priority { get; set; }

    public OperationRequestDto(OperationRequest operationRequest)
    {

        this.OperationRequestID = operationRequest.operationRequestID.AsString();
        this.PatientID = operationRequest.patientID.ToString();
        this.DoctorID = operationRequest.doctorID.ToString();
        this.OperationTypeID = operationRequest.operationTypeID.ToString();
        this.DeadlineDate = operationRequest.deadlineDate.ToString();
        this.Priority = operationRequest.priority.ToString();
    }

    [JsonConstructor]
    public OperationRequestDto(string OperationRequestID, string PatientID, string DoctorID, string OperationTypeID, string DeadlineDate, string Priority)
    {
        this.OperationRequestID = OperationRequestID;
        this.PatientID = PatientID;
        this.DoctorID = DoctorID;
        this.OperationTypeID = OperationTypeID;
        this.DeadlineDate = DeadlineDate;
        this.Priority = Priority;
    }

}
