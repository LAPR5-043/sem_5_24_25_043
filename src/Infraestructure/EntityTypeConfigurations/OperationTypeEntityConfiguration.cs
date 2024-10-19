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

            builder.HasKey(p => p.operationTypeName);

            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new OperationTypeName(v))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.operationTypeName)
                .HasConversion(
                    v => v.AsString(),
                    v => new OperationTypeName(v))
                .IsRequired();
            
            builder.Property(t => t.estimatedDuration)
                .HasConversion(
                    v => v.ToString(),        
                    v => parseEstimatedDuration(v)
                )
                .IsRequired();

            builder.Property(t => t.isActive)
                .IsRequired();

              builder.Property(t => t.specializations)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, int>()
                )
                .IsRequired();
                    
                  
            
   }

    private EstimatedDuration parseEstimatedDuration(string v)
    {
        return new EstimatedDuration(int.Parse(v.Split(":")[0]), int.Parse(v.Split(":")[1]));
    }
}


