using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Domain.PatientAggregate;
using src.Domain.PatientAggregate;
using Infrastructure.Converters;

namespace Infrastructure.PatientRepository
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(p => p.medicalRecordNumber);


            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new MedicalRecordNumber(v)
                )
                .HasValueGenerator<MedicalRecordNumberGenerator>()
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.medicalRecordNumber)
                .HasConversion(new MedicalRecordNumberConverter())
                .IsRequired();


            builder.Property(p => p.firstName)
                .HasConversion(
                    v => v.ToString(),
                    v => new PatientFirstName(v)
                )
                .IsRequired();

            builder.Property(p => p.lastName)
                .HasConversion(
                    v => v.ToString(),
                    v => new PatientLastName(v)
                )
                .IsRequired();

            builder.Property(p => p.fullName)
                .HasConversion(
                    v => v.ToString(),
                    v => ConvertToPatientFullName(v)
                )
                .IsRequired();

            builder.Property(p => p.email)
                .HasConversion(
                    v => v.email,
                    v => new PatientEmail(v)
                )
                .IsRequired();

            builder.Property(p => p.phoneNumber)
                .HasConversion(
                    v => v.phoneNumber,
                    v => new PatientPhoneNumber(v)
                )
                .IsRequired();
                
            builder.Property(p => p.emergencyContact)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<EmergencyContact>(v, (JsonSerializerOptions)null)
                )
                .IsRequired();

            builder.Property(p => p.dateOfBirth)
                .HasConversion(
                    v => v.dateOfBirth.Day + "/" + v.dateOfBirth.Month + "/" + v.dateOfBirth.Year,
                    v => ConvertToDateOfBirth(v)
                )
                .IsRequired();

            builder.Property(p => p.gender)
                .HasConversion(
                    v => v.ToString(),
                    v => GenderExtensions.FromString(v)
                )
                .IsRequired();

            builder.Property(p => p.allergiesAndConditions)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions)null) : "[]", // Serialize or assign empty JSON array
                v => JsonSerializer.Deserialize<List<AllergiesAndConditions>>(v, (JsonSerializerOptions)null) ?? new List<AllergiesAndConditions>() // Ensure a list is returned
            )
            .IsRequired();

            builder.Property(p => p.appointmentHistory)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions)null) : "[]", // Serialize or assign empty JSON array
                v => JsonSerializer.Deserialize<AppointmentHistory>(v, (JsonSerializerOptions)null) ?? new AppointmentHistory() // Ensure a list is returned
            )
            .IsRequired();


            builder.HasIndex(p => p.email).IsUnique();
            builder.HasIndex(p => p.phoneNumber).IsUnique();
            builder.HasIndex(p => p.medicalRecordNumber).IsUnique();
        }

        private static DateOfBirth ConvertToDateOfBirth(string v)
        {
            var parts = v.Split('/');
            return new DateOfBirth(parts[0], parts[1], parts[2].Split(' ')[0]);
        }

        private static PatientFullName ConvertToPatientFullName(string v)
        {
            var parts = v.Split(' ');
            return new PatientFullName(parts[0], parts[1]);
        }
    }
}