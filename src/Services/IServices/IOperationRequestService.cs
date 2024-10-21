using System;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationRequestService
    {
        Task<bool> DeleteOperationRequestAsync(int id);
        Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto);

        Task<OkObjectResult> getAllOperationRequestsAsync();
        //Task<ActionResult<IEnumerable<OperationRequestDto>>> getOperationRequestsFilteredAsync(string? firstName, string? lastName, string? operationType, string? priority, string? sortBy);
        Task<OperationRequestDto> getOperationRequestAsync(string id);
    }
}