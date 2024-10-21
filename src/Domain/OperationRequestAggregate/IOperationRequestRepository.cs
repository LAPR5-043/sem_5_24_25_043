using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.Shared;

namespace Domain.OperationRequestAggregate
{
    public interface IOperationRequestRepository : IRepository<OperationRequest, OperationRequestID>
    {
        Task updateAsync(OperationRequest operationRequest);
    }
}