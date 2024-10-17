using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Infrastructure.Shared;

namespace src.Infrastructure.OperationTypes
{
internal class SpecializationEntityTypeConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            //builder.ToTable("Categories", SchemaNames.DDDSample1);
            builder.HasKey(b => b.SpecializationId);

            var specializationNameConverter = new ValueConverter<SpecializationName, string>(
                v => v.AsString(),
                v => new SpecializationName(v)
            );

            var specializationDescConverter = new ValueConverter<SpecializationDescription, string>(
                v => v.ToString(),
                v => new SpecializationDescription(v)
            );

            builder.Property(b => b.SpecializationId)
                   .HasColumnName("SpecializationId")
                   .IsRequired()
                   .HasConversion(specializationNameConverter);

            builder.Property(b => b.description)
                   .HasColumnName("Description")
                   .IsRequired()
                   .HasConversion(specializationDescConverter);
        }

    }
}