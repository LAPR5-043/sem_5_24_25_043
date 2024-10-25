using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationRequestService
    {
        Task<bool> CreateOperationRequestAsync(OperationRequestDto operationRequestDto);
        Task<ActionResult<IEnumerable<OperationRequestDto>>> GetOperationRequestFilteredAsync(string? firstName, string? lastName, string? operationType, string? priority, string? status, string? sortBy);
        Task<IActionResult> GetOperationRequestByIdAsync(string id);
        Task<bool> DeleteOperationRequestAsync(int id, string doctorEmail);
        Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto, string email);
    }
}