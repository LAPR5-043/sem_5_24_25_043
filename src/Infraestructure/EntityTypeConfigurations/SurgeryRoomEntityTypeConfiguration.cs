using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using Infrastructure.Converters;
using Microsoft.CodeAnalysis.Elfie.Model;
using src.Domain.SurgeryRoomAggregate;

public class SurgeryRoomEntityTypeConfiguration : IEntityTypeConfiguration<SurgeryRoom>
    {
        public void Configure(EntityTypeBuilder<SurgeryRoom> builder)
        {
            builder.ToTable("SurgeryRooms");

            builder.HasKey(p => p.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new RoomId(v))
                .IsRequired().HasValueGenerator<SurgeryRoomIDGenerator>();

            builder.Property(t => t.RoomID)
                .HasConversion(
                    v => v.AsString(),
                    v => new RoomId(v))
                .IsRequired();


            builder.Property(t => t.Name)
                .IsRequired();                    
                  
            builder.HasIndex(t => t.Name).IsUnique();
   }

 
}