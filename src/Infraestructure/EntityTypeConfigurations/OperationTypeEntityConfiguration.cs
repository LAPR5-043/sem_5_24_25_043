using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using Infrastructure.Converters;
using Microsoft.CodeAnalysis.Elfie.Model;

public class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            builder.ToTable("OperationTypes");

            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),        
                    v => new OperationTypeName(v)
                )
                .IsRequired();
            
            builder.Property(t => t.operationTypeName)
                .HasConversion(
                    v => v.AsString(),        
                    v => new OperationTypeName(v)
                )
                .IsRequired();
            
            builder.Property(t => t.estimatedDuration)
                .HasConversion(
                    v => v.ToString(),        
                    v => parseEstimatedDuration(v)
                )
                .IsRequired();

            builder.Property(t => t.isActive)
                .IsRequired();

            builder.Property(t => t.specialization)
                .IsRequired();          
                    
                    
            
   }

    private EstimatedDuration parseEstimatedDuration(string v)
    {
        return new EstimatedDuration(int.Parse(v.Split(":")[0]), int.Parse(v.Split(":")[1]));
    }
}


