using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using Infrastructure.Converters;
using Microsoft.CodeAnalysis.Elfie.Model;
using src.Domain.SurgeryRoomAggregate;

public class SpecializationEntityTypeConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.ToTable("Specializations");

            builder.HasKey(p => p.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new SpecializationName(v))
                .IsRequired();

            builder.Property(t => t.specializationName)
                .HasConversion(
                    v => v.AsString(),
                    v => new SpecializationName(v))
                .IsRequired();


            builder.Property(t => t.specializationDescription)
                .HasConversion(
                    v => v.ToString(),
                    v => new SpecializationDescription(v))
                .IsRequired();                    
                  
            
   }

 
}