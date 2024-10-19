using System;
using Domain.OperationTypeAggregate;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationTypeService
    {
        Task<bool> createOperationTypeAsync(OperationTypeDto operationType);
        Task<bool> deactivateOperationTypeAsync(string id);
        Task<ActionResult<IEnumerable<OperationTypeDto>>> getAllOperationTypesAsync();
        Task<ActionResult<IEnumerable<OperationTypeDto>>> getFilteredOperationTypesAsync(string name, string specialization, string status);
        Task<ActionResult<OperationType>> getOperationTypeAsync(string id);
    }
}