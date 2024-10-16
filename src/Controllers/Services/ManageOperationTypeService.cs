using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sem_5_24_25_043.Models;
using src.Infrastructure.OperationTypes;
using AppContext= src.Models.AppContext;

namespace src.Controllers.Services;

public class ManageOperationTypeService 
{
    private readonly AppContext _context;
    private OperationTypeRepository operationTypeRepository = Repositories.GetInstance().getOperationTypeRepository();

    public ManageOperationTypeService( AppContext context)
    {
        _context = context;
    }
    
    public async Task<bool> deactivateOperationTypeAsync(OperationType operationType, bool isActive){
        operationType.changeOperationStatus(isActive);
        try
        {
            await operationTypeRepository.UpdateAsync(operationType);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!operationTypeRepository.OperationTypeExists(operationType.operationTypeId()))
            {
                return false;
            }
            else
            {
                throw;
            }
        }

        return true;
    }
    
    public async Task<ActionResult<IEnumerable<OperationType>>> getAllOperationTypesAsync()
    {
        var result = await operationTypeRepository.GetAllAsync();
        return new OkObjectResult(result);
    }

    public async Task<ActionResult<OperationType>> getOperationTypeAsync(long id)
    {
       
         return  await operationTypeRepository.GetByIdAsync(id);
    }

    public void deleteOperationType(ActionResult<OperationType> op)
    {
        if (op.Value != null)
        {
            operationTypeRepository.Remove(op.Value);

        }
    }

    public async Task<bool> updateOperationTypeAsync(OperationType? value, OperationTypeDto operationTypeDto)
    {
        value.changeOperationType(operationTypeDto.OperationTypeName);
        value.changeOperationDuration(operationTypeDto.Hours, operationTypeDto.Minutes);
        value.changeOperationStatus(operationTypeDto.IsActive);
       


        try
        {
            await operationTypeRepository.UpdateAsync(value);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!operationTypeRepository.OperationTypeExists(value.operationTypeId()))
            {
                return false;
            }
            else
            {
                throw;
            }
        }

        return true;
    }


    public async Task<OperationType> AddOperationTypeAsync(OperationTypeDto operationTypeDto)
    {
        OperationType op = new OperationType(
                operationTypeDto.OperationTypeID,
                operationTypeDto.IsActive,
                operationTypeDto.OperationTypeName,
                operationTypeDto.Hours,
                operationTypeDto.Minutes
            );
           
        operationTypeRepository.AddAsync(op);   


        return op;
    }
}