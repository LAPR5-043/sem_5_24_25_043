using Domain.OperationRequestAggregate;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using AppContext = src.Models.AppContext;

public class OperationRequestIDGenerator : ValueGenerator<OperationRequestID>
{
    public override OperationRequestID Next(EntityEntry entry)
    {
        var context = (AppContext)entry.Context;

        var latestNumber = context.OperationRequests
            .AsEnumerable()
            .OrderByDescending(or => or.operationRequestID)
            .Select(or => or.operationRequestID)
            .FirstOrDefault();

        var sequentialNumber = latestNumber != null ? int.Parse(latestNumber.Value) + 1 : 1;

        var newNumber = $"{sequentialNumber:D6}";

        OperationRequestID operationRequestID = new OperationRequestID(newNumber);

        entry.Property("operationRequestID").CurrentValue = operationRequestID;

        return operationRequestID;
    }

    public override bool GeneratesTemporaryValues => false;
}