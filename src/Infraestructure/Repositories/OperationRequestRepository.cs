using Domain.OperationRequestAggregate;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;

/// <summary>
/// Operation Request Repository
/// </summary>
public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    /// <summary>
    /// App context
    /// </summary>
    private readonly AppContext context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public OperationRequestRepository(AppContext context) : base(context.OperationRequests)
    {
        this.context = context;
    }

    public  Task updateAsync(OperationRequest operationRequest){
        context.OperationRequests.Update(operationRequest);
        return context.SaveChangesAsync();
    }
}