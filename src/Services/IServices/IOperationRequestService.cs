using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationRequestService
    {
        Task<bool> CreateOperationRequestAsync(OperationRequestDto operationRequestDto, string email);
        Task<List<OperationRequestDto>> GetOperationRequestFilteredAsync(string? doctorID, string? patientID, string? patientFirstName, string? patientLastName, string? operationType, string? priority, string? sortBy);
        Task<List<OperationRequestDto>> GetDoctorOperationRequestsAsync(string doctorEmail, string? patientID, string? patientFirstName, string? patientLastName, string? operationType, string? priority, string? sortBy);
        Task<List<OperationRequestDto>> GetOperationRequestByPatientIdAsync(string id);
        Task<bool> DeleteOperationRequestAsync(int id, string doctorEmail);
        Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto, string email);
        Task<OperationRequestDto> GetOperationRequestByIdAsync(int id);
        Task<List<OperationRequest>> GetAllOperationRequestsAsync();
        
    }
}