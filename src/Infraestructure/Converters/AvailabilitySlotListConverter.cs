using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NuGet.Packaging.Signing;
using System.Security.Cryptography.Pkcs;


public class AvailabilitySlotListConverter : ValueConverter<AvailabilitySlots, string>
{
    public AvailabilitySlotListConverter() : base(
        v => string.Join(';', v.slots.Select(slot => $"{slot.startDate},{slot.endDate},{slot.occupied}")),
        v => ParseAvailabilitySlots(v)) 
    {}

    private static AvailabilitySlots ParseAvailabilitySlots(string input)
    {
        return new AvailabilitySlots(
            input.Split(';', StringSplitOptions.RemoveEmptyEntries)
                 .Select(slotString =>
                 {
                     var parts = slotString.Split(',');
                     return new TimeSlot(DateTime.Parse(parts[0]), DateTime.Parse(parts[1]), bool.Parse(parts[2]));
                 }).ToList()
        );
    }
}

