using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using AppContext = src.Models.AppContext;


public class SurgeryRoomIDGenerator : ValueGenerator<RoomId>
{
    public override RoomId Next(EntityEntry entry)
    {
        var context = (AppContext)entry.Context;


        var latestNumber = context.SurgeryRooms
            .AsEnumerable()
            .OrderByDescending(s => s.Id)
            .Select(s => s.Id)
            .FirstOrDefault();

        var sequentialNumber = latestNumber != null ? int.Parse(latestNumber.Value.Substring(2)) + 1 : 1;

        var newNumber = $"or{sequentialNumber}";

        RoomId roomId = new RoomId(newNumber);

        entry.Property("Id").CurrentValue = roomId  ;
        entry.Property("RoomID").CurrentValue = roomId  ;

        return new RoomId(newNumber);
    }

    public override bool GeneratesTemporaryValues => false;
}