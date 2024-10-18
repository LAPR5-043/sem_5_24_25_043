using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;


namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs");
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),        
                    v => new StaffID(v)
                )
                .HasValueGenerator<StaffIDGenerator>()
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            builder.Property(t => t.staffID)

                .HasConversion(
                    v => v.AsString(),
                    v => new StaffID(v)
                )
                .IsRequired();
                    
                    
            builder.Property(t => t.licenseNumber)
                .HasConversion(
                    v => v.ToString(),
                    v => new LicenseNumber(v)
                )
                .IsRequired();

            builder.Property(t => t.firstName)
                .HasConversion(
                    v => v.ToString(),
                    v => new StaffFirstName(v)
                )
                .IsRequired();

            builder.Property(t => t.lastName)
                .HasConversion(
                    v => v.ToString(),
                    v => new StaffLastName(v)
                )
                .IsRequired();

            builder.Property(t => t.fullName)
                .HasConversion(
                    v => v.ToString(), 
                    v => ConvertToStaffFullName(v) 
                )
                .IsRequired();

            // The method to handle the conversion from string to StaffFullName


            builder.Property(t => t.email)
                .HasConversion(
                    v => v.ToString(),
                    v => new StaffEmail(v)
                )
                .IsRequired();
            
            builder.Property(t => t.phoneNumber)
                .HasConversion(
                    v => v.ToString(),
                    v => new StaffPhoneNumber(v)
                )
                .IsRequired();

            
            builder.Property(t => t.availabilitySlots)
                .HasConversion(new AvailabilitySlotListConverter())
                .IsRequired();

            
            builder.Property(t => t.specializationID)
                .IsRequired();


            builder.HasIndex(t => t.email).IsUnique();
            builder.HasIndex(t => t.licenseNumber).IsUnique();
            builder.HasIndex(t => t.phoneNumber).IsUnique();
    }
               private static StaffFullName ConvertToStaffFullName(string v)
            {
                var parts = v.Split(',');
                return new StaffFullName(new StaffFirstName(parts[0]), new StaffLastName(parts[1]));
            }
    }


}