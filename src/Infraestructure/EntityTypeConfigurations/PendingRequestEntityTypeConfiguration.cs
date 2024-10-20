using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using Infrastructure.Converters;
using Microsoft.CodeAnalysis.Elfie.Model;


public class PendingRequestEntityTypeConfiguration : IEntityTypeConfiguration<PendingRequest>
    {
        public void Configure(EntityTypeBuilder<PendingRequest> builder)
        {
            builder.ToTable("PendingRequests");

            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),        
                    v => new LongId(long.Parse(v))
                )
                .HasValueGenerator<LongIDGenerator>()
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Property(t => t.requestID)
                .HasConversion(
                    v => v.AsString(),        
                    v => new LongId(long.Parse(v))
                )
                .IsRequired();
            
            builder.Property(t => t.attributeName)
                .IsRequired();

            builder.Property(t => t.oldValue)
                .IsRequired();

            builder.Property(t => t.pendingValue)
                .IsRequired();        

            builder.Property(t => t.userId)
                .IsRequired();                       
                    
            
   }


}