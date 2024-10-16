using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Infrastructure.Shared;

namespace src.Infrastructure.OperationTypes
{
internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            //builder.ToTable("Categories", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("Name(Id)").IsRequired();
            // Configure private fields
            builder.Property<string>("name")
                   .HasField("firstName")
                   .HasColumnName("FirstName")
                   .IsRequired();

            builder.Property<string>("lastName")
                   .HasField("Value")
                   .HasColumnName("LastName")
                   .IsRequired();

            builder.Property<string>("license")
                   .HasField("LicenseNumberValue")
                   .HasColumnName("LicenseNumber")
                   .IsRequired();

            builder.Property<bool>("activeStatus")
                   .HasField("active")
                   .HasColumnName("ActiveStatus")
                   .IsRequired();

            builder.Property<string>("contactInformation")
                   .HasField("contactInformation")
                   .HasColumnName("ContactInformation")
                   .IsRequired();

            builder.Property<string>("specialization")
                   .HasField("name")
                   .HasField("description")
                   .HasColumnName("Specialization")
                   .IsRequired();

            
            builder.Property<string>("specializationId")
                .HasColumnName("SpecializationId")
                .IsRequired();

            builder.HasOne<SpecializationName>("specializationId")
                .WithOne()
                .HasForeignKey("specializationId");


            //builder.Property<bool>("_active").HasColumnName("Active");
        }

    }
}