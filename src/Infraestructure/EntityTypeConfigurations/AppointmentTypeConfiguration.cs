using Domain.AppointmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;

public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.appointmentID);

        builder.Property(a => a.Id)
            .HasConversion(
                v => v.ToString(),
                v => new AppointmentID(v)
            )
            .HasValueGenerator<AppointmentIDGenerator>()
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(a => a.appointmentID)
            .HasConversion(
                v => v.ToString(),
                v => new AppointmentID(v)
            )
            .IsRequired();

        builder.Property(a => a.requestID)
            .IsRequired();

        builder.Property(a => a.roomID)
            .IsRequired();

        builder.Property(a => a.dateAndTime)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => DateAndTimeFromString(v)
            );

        builder.Property(a => a.status)
            .HasConversion(
                v => v.ToString(),
                v => StatusExtensions.FromString(v)
            )
            .IsRequired();
    }

    private static DateAndTime DateAndTimeFromString(string v)
    {
        var parts = v.Split(",");
        return new DateAndTime(parts[0], parts[1], parts[2]);
    }
}