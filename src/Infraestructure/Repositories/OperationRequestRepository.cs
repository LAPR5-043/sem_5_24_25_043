using Domain.OperationRequestAggregate;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    private readonly AppContext context;

    public OperationRequestRepository(AppContext context) : base(context.OperationRequests)
    {
        this.context = context;
    }
}