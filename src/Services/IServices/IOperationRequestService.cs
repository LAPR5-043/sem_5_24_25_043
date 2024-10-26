using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationRequestService
    {
        Task<bool> CreateOperationRequestAsync(OperationRequestDto operationRequestDto, string email);
        Task<ActionResult<IEnumerable<OperationRequestDto>>> GetOperationRequestFilteredAsync(string? doctorID, string? patientID, string? patientFirstName, string? patientLastName, string? operationType, string? priority, string? sortBy);
        Task<ActionResult<IEnumerable<OperationRequestDto>>> GetDoctorOperationRequestsAsync(string doctorEmail, string? patientID, string? patientFirstName, string? patientLastName, string? operationType, string? priority, string? sortBy);
        Task<ActionResult<IEnumerable<OperationRequestDto>>> GetOperationRequestByPatientIdAsync(string id);
        Task<bool> DeleteOperationRequestAsync(int id, string doctorEmail);
        Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto, string email);
    }
}