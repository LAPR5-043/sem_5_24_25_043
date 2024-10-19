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