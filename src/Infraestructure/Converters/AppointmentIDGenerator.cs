using Domain.AppointmentAggregate;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using AppContext = src.Models.AppContext;

public class AppointmentIDGenerator : ValueGenerator<AppointmentID>
{
    public override AppointmentID Next(EntityEntry entry)
    {
        var context = (AppContext)entry.Context;

        var latestNumber = context.Appointments
            .AsEnumerable()
            .OrderByDescending(a => a.appointmentID)
            .Select(a => a.appointmentID)
            .FirstOrDefault();

        var sequentialNumber = latestNumber != null ? int.Parse(latestNumber.Value) + 1 : 1;

        var newNumber = $"{sequentialNumber:D6}";

        AppointmentID appointmentID = new AppointmentID(newNumber);

        entry.Property("appointmentID").CurrentValue = appointmentID;

        return appointmentID;
    }

    public override bool GeneratesTemporaryValues => false;
}

    