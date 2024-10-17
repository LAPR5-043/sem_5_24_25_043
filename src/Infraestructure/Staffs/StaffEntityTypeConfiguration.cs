using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Infrastructure.Shared;

namespace src.Infrastructure.OperationTypes
{
    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // Configure StaffId as the primary key
            //builder.HasKey(b => b.Id, e => e.LicenseNumberValue);

            // Configure the foreign key for Specialization
            builder.HasOne<Specialization>()
                   .WithMany()
                   .HasForeignKey(b => b.specializationId)
                   .HasAnnotation("Relationship", "OneToOne");

            // Create a value converter for LicenseNumber
            var licenseNumberConverter = new ValueConverter<LicenseNumber, string>(
                v => v.AsString(), // Convert LicenseNumberValue to string
                v => new LicenseNumber(v)// Convert string to LicenseNumberValue
            );

            // Create a value converter for StaffEmail
            var emailConverter = new ValueConverter<StaffEmail, string>(
                v => v.email, // Convert StaffEmail to string
                v => new StaffEmail(v) // Convert string to StaffEmail
            );

            // Create a value converter for StaffPhoneNumber
            var phoneConverter = new ValueConverter<StaffPhoneNumber, string>(
                v => v.phoneNumber, // Convert StaffPhoneNumber to string
                v => new StaffPhoneNumber(v) // Convert string to StaffPhoneNumber
            );

            // Apply the value converter to the LicenseNumber property
            builder.Property(b => b.Id)
                   .HasConversion(licenseNumberConverter)
                   .HasColumnName("LicenseNumber")
                   .IsRequired().HasAnnotation("Key", true);

            
            builder.OwnsOne(b => b.StaffId, f =>
            {
                f.Property(e => e.LicenseNumberValue).HasColumnName("LicenseNumber").IsRequired();
            });

            builder.OwnsOne(b => b.firstName, f =>
            {
                f.Property(e => e.name).HasColumnName("FirstName").IsRequired();
            });
            builder.OwnsOne(b => b.lastName, f =>
            {
                f.Property(e => e.Value).HasColumnName("LastName").IsRequired();
            });
            builder.OwnsOne(b => b.activeStatus, ia =>
            {
                ia.Property(e => e.active).HasColumnName("Active").IsRequired().HasConversion(new IsActiveValueConverter());
            });

            builder.OwnsOne(b => b.contactInformation, ci =>
            {
                ci.Property(e => e.phoneNumber)
                    .HasColumnName("PhoneNumber")
                    .IsRequired()
                    .HasConversion(phoneConverter);
                ci.Property(e => e.email)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasConversion(emailConverter);
            });

          //  builder.OwnsOne(b => b.specializationId, a => a.Property(e => e.Value).HasColumnName("SpecializationId").IsRequired());

            // Ignore availability if it's not needed
            builder.Ignore(b => b.availability);
            
        }
    }
}