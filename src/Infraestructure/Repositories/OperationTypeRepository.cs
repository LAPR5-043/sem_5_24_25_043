using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;


public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeName>
{
    public OperationTypeRepository(DbSet<OperationType> objs) : base(objs)
    {
        
    }
}