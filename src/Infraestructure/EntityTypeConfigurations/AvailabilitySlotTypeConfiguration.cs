using System.Text.Json;
using Domain.AppointmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;
using src.Domain.AvailabilitySlotAggregate;

public class AvailabilitySlotTypeConfiguration : IEntityTypeConfiguration<AvailabilitySlot>
{
    public void Configure(EntityTypeBuilder<AvailabilitySlot> builder)
    {
        builder.ToTable("AvailabilitySlots");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                v => v.ToString(),
                v => new StaffID(v)
            )
            .IsRequired();
        builder.Property(a => a.StaffID)
            .HasConversion(
                v => v.ToString(),
                v => new StaffID(v)
            )
            .IsRequired();

        builder.Property(t => t.Slots)
        .HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
            v => JsonSerializer.Deserialize<Dictionary<int, Slot>>(v, new JsonSerializerOptions()) ?? new Dictionary<int, Slot>()
        )
        .IsRequired();
       
    }


}