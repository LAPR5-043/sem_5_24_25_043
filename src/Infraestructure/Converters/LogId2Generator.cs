using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using AppContext = src.Models.AppContext;


public class LongId2Generator : ValueGenerator<LongId>
{
    public override LongId Next(EntityEntry entry)
    {
        var context = (AppContext)entry.Context;


        var latestNumber = context.Logs
            .AsEnumerable()
            .OrderByDescending(s => s.Id)
            .Select(s => s.Id)
            .FirstOrDefault();

        var sequentialNumber = latestNumber != null ? int.Parse(latestNumber.AsString()) + 1 : 1;
        LongId longID = new LongId(sequentialNumber);


        entry.Property(nameof(Log.logId)).CurrentValue = longID  ;

        context.SaveChanges();

        return new LongId(sequentialNumber);
    }

    public override bool GeneratesTemporaryValues => false;
}