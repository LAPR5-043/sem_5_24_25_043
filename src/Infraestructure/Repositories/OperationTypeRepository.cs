using Domain.OperationTypeAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using AppContext = src.Models.AppContext;



public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeName>, IOperationTypeRepository
{
    private readonly AppContext context;

    public OperationTypeRepository(AppContext context) : base(context.OperationTypes)
    {
        this.context = context;
    }

    public bool OperationTypeExists(string name)
    {
        return context.OperationTypes.Any(e => e.operationTypeName.Equals(new OperationTypeName(name)));
    }

    public async Task UpdateAsync(OperationType operationType)
    {
        context.Entry(operationType).State = EntityState.Modified;
    }
}