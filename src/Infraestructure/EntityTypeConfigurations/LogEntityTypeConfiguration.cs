using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using Infrastructure.Converters;
using Microsoft.CodeAnalysis.Elfie.Model;


public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),        
                    v => new LongId(long.Parse(v))
                )
                .HasValueGenerator<LongIDGenerator>()
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Property(t => t.logId)
                .HasConversion(
                    v => v.AsString(),        
                    v => new LongId(long.Parse(v))
                )
                .HasValueGenerator<LongId2Generator>()
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Property(t => t.action)
                .IsRequired();

            builder.Property(t => t.timestamp)
                .IsRequired()
                .HasConversion(new DateTimeConverter());

            builder.Property(t => t.email)
                .IsRequired();          
                    
                    
            
   }
}


