using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using AppContext = src.Models.AppContext;

namespace src.Infrastructure.Converters
{

public class RequestsIDGenerator : ValueGenerator<LongId>
{
    public override LongId Next(EntityEntry entry)
    {
        var context = (AppContext)entry.Context;


        var latestNumber = context.PendingRequests
            .AsEnumerable()
            .OrderByDescending(s => s.Id)
            .Select(s => s.Id)
            .FirstOrDefault();


        entry.Property(nameof(PendingRequest.requestID)).CurrentValue =entry.Property(nameof(PendingRequest.Id)).CurrentValue  ;

        long sequentialNumber = long.Parse(entry.Property(nameof(PendingRequest.requestID)).CurrentValue.ToString());

        context.SaveChanges();

        return new LongId(sequentialNumber);
    }

    public override bool GeneratesTemporaryValues => false;
}

}