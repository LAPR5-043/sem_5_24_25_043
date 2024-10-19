using System;
using Domain.OperationTypeAggregate;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationTypeService
    {
        Task<bool> CreateOperationTypeAsync(OperationTypeDto operationType);
        Task<bool> deactivateOperationTypeAsync(string id);
        Task<ActionResult<IEnumerable<OperationType>>> getAllOperationTypesAsync();
        Task<ActionResult<OperationType>> getOperationTypeAsync(string id);
    }
}