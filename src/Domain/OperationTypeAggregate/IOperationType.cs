using src.Domain.Shared;

namespace Domain.OperationTypeAggregate
{
    public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeName>
    {
    }
}