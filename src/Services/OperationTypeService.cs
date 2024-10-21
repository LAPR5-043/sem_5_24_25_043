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
    private readonly ILogService logService;


    public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository, ILogService logService)
    {
        this.unitOfWork = unitOfWork;
        this.operationTypeRepository = operationTypeRepository;
        this.logService = logService;
    }

    public async Task<bool> createOperationTypeAsync(OperationTypeDto operationType)
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
        // TODO: Falta
        await logService.CreateLogAsync("Operation type created. ID: " + newOperationType.Id.Value, "colocar@emailtoken.aqui");

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

    // GET: api/OperationType/Filters?name=&specialization=&status=&
    public async Task<ActionResult<IEnumerable<OperationTypeDto>>> getFilteredOperationTypesAsync(string name, string specialization, string status)
    {
        var operationTypesList = await operationTypeRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(name))
        {
            operationTypesList = operationTypesList.Where(o => o.operationTypeName.Value.Contains(name)).ToList();
        }

        if (!string.IsNullOrEmpty(specialization))
        {
            operationTypesList = operationTypesList.Where(o => o.specializations.ContainsKey(specialization)).ToList();
        }

        if (!string.IsNullOrEmpty(status))
        {
            operationTypesList = operationTypesList.Where(o => o.isActive == bool.Parse(status)).ToList();
        }

        var operationTypeDtos = operationTypesList.Select(o => new OperationTypeDto(o)).ToList();
        return operationTypeDtos;
    }
    public async Task<ActionResult<IEnumerable<OperationTypeDto>>> getAllOperationTypesAsync()
    {
        var result = await operationTypeRepository.GetAllAsync();
        IEnumerable<OperationTypeDto> operationTypeDtos = new List<OperationTypeDto>();
        
        if (result != null)
        {
            operationTypeDtos = result.Select(o => new OperationTypeDto(o)).ToList();
        }
        return operationTypeDtos.ToList();
    }


    public async Task<ActionResult<OperationType>> getOperationTypeAsync(string id)
    {
       
         return  await operationTypeRepository.GetByIdAsync(new OperationTypeName(id));
    }

    public async Task<bool> editOperationTypeAsync(string id, OperationTypeDto operationTypeDTO)
    {
        var operationTypeToEdit = await operationTypeRepository.GetByIdAsync(new OperationTypeName(id));
        if (operationTypeToEdit == null)
        {
            return false;
        }
        operationTypeToEdit.operationTypeName = new OperationTypeName(operationTypeDTO.OperationTypeName);
        operationTypeToEdit.estimatedDuration = new EstimatedDuration(int.Parse(operationTypeDTO.EstimatedDurationHours), int.Parse(operationTypeDTO.EstimatedDurationMinutes));
        operationTypeToEdit.isActive = operationTypeDTO.IsActive;
        operationTypeToEdit.specializations = operationTypeDTO.Specializations.ToDictionary(s => s.Key, s => int.Parse(s.Value));
        await operationTypeRepository.UpdateAsync(operationTypeToEdit);
        await unitOfWork.CommitAsync();
        return true;
    }
}