using System;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IOperationTypeService
    {
        Task<bool> deactivateOperationTypeAsync(string id);
        Task<ActionResult<IEnumerable<OperationType>>> getAllOperationTypesAsync();
        Task<ActionResult<OperationType>> getOperationTypeAsync(string id);
    }
}