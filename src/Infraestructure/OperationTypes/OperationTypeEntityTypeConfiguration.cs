using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Infrastructure.Shared;

namespace src.Infrastructure.OperationTypes
{
internal class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx OperationTypeIDValueConverter
            
            //builder.ToTable("Categories", SchemaNames.DDDSample1);
            builder.HasKey(b => b.operationTypeID);
            builder.Property(b => b.operationTypeID)
                   .HasColumnName("Id")
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasConversion(new OperationTypeIDValueConverter());

            builder.OwnsOne(b => b.operationType, ot =>
            {
                ot.Property(e => e.name).HasColumnName("name").IsRequired();
            });
            builder.OwnsOne(b => b.estimatedDuration, od =>
            {
                od.Property(e => e.hours).HasColumnName("hours").IsRequired();
                od.Property(e => e.minutes).HasColumnName("minutes").IsRequired();
            });
            builder.OwnsOne(b => b.isActive, ia =>
            {
                ia.Property(e => e.active).HasColumnName("Active").IsRequired().HasConversion(new IsActiveValueConverter());
            });

            //builder.Property<bool>("_active").HasColumnName("Active");
        }

    }
}