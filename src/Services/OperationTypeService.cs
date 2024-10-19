using Domain.OperationTypeAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Domain.Shared;
using src.Services.IServices;
using AppContext= src.Models.AppContext;

namespace src.Controllers.Services;

public class OperationTypeService : IOperationTypeService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IOperationTypeRepository operationTypeRepository;


    public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository)
    {
        this.unitOfWork = unitOfWork;
        this.operationTypeRepository = operationTypeRepository;
    }

    public async Task<bool> CreateOperationTypeAsync(OperationTypeDto operationType)
    {
        if (operationType == null)
        {
            throw new ArgumentNullException(nameof(operationType) , "Operation type data is null.");
        }

        if (operationTypeRepository.OperationTypeExists(operationType.OperationTypeName))
        {
            throw new InvalidOperationException("Operation type already exists.");
        }

        var newOperationType = new OperationType();
        newOperationType.Id = new OperationTypeName(operationType.OperationTypeName);
        newOperationType.operationTypeName = new OperationTypeName(operationType.OperationTypeName);
        newOperationType.estimatedDuration = new EstimatedDuration(int.Parse(operationType.EstimatedDurationHours), int.Parse(operationType.EstimatedDurationMinutes));
        newOperationType.isActive = operationType.IsActive;
        newOperationType.specializations = operationType.Specializations.ToDictionary(s => s.Key, s => int.Parse(s.Value));
    
        await operationTypeRepository.AddAsync(newOperationType);
        await unitOfWork.CommitAsync();

        return newOperationType != null;
    }

    public async Task<bool> deactivateOperationTypeAsync(string id){
        var operationType = await operationTypeRepository.GetByIdAsync(new OperationTypeName(id));
        if (operationType == null)
        {
            return false;
        }
        operationType.changeStatus();

        await operationTypeRepository.UpdateAsync(operationType);
        await unitOfWork.CommitAsync();
     

        return true;
    }
    
    public async Task<ActionResult<IEnumerable<OperationType>>> getAllOperationTypesAsync()
    {
        var result = await operationTypeRepository.GetAllAsync();
        return new OkObjectResult(result);
    }

    public async Task<ActionResult<OperationType>> getOperationTypeAsync(string id)
    {
       
         return  await operationTypeRepository.GetByIdAsync(new OperationTypeName(id));
    }



}