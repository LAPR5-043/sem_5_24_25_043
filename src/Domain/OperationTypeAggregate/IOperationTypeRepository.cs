using src.Domain.Shared;


public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeName>
{
    Task UpdateAsync(OperationType operationType);
}
