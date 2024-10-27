using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Domain.PatientAggregate;
using src.Domain;
using Infrastructure.Converters;
using System.Text.Encodings.Web;

namespace Infrastructure.PatientRepository
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(p => p.MedicalRecordNumber);


            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new MedicalRecordNumber(v)
                )
                .HasValueGenerator<MedicalRecordNumberGenerator>()
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.MedicalRecordNumber)
                .HasConversion(new MedicalRecordNumberConverter())
                .IsRequired();


            builder.Property(p => p.FirstName)
                .HasConversion(
                    v => v.ToString(),
                    v => new PatientFirstName(v)
                )
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasConversion(
                    v => v.ToString(),
                    v => new PatientLastName(v)
                )
                .IsRequired();

            builder.Property(p => p.FullName)
                .HasConversion(
                    v => v.ToString(),
                    v => ConvertToPatientFullName(v)
                )
                .IsRequired();

            builder.Property(p => p.Email)
                .HasConversion(
                    v => v.Value,
                    v => new PatientEmail(v)
                )
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasConversion(
                    v => v.Value,
                    v => new PatientPhoneNumber(v)
                )
                .IsRequired();
                
            builder.Property(p => p.EmergencyContact)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }),
                v => JsonSerializer.Deserialize<EmergencyContact>(v, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                })
            )
            .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .HasConversion(
                    v => v.Value.Day + "/" + v.Value.Month + "/" + v.Value.Year,
                    v => ConvertToDateOfBirth(v)
                )
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => GenderExtensions.FromString(v)
                )
                .IsRequired();

            builder.Property(p => p.AllergiesAndConditions)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions)null) : "[]", // Serialize or assign empty JSON array
                v => JsonSerializer.Deserialize<List<AllergiesAndConditions>>(v, (JsonSerializerOptions)null) ?? new List<AllergiesAndConditions>() // Ensure a list is returned
            )
            .IsRequired();

            builder.Property(p => p.AppointmentHistory)
            .HasConversion(
                v => v != null ? JsonSerializer.Serialize(v, (JsonSerializerOptions)null) : "[]", // Serialize or assign empty JSON array
                v => JsonSerializer.Deserialize<AppointmentHistory>(v, (JsonSerializerOptions)null) ?? new AppointmentHistory() // Ensure a list is returned
            )
            .IsRequired();


            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.PhoneNumber).IsUnique();
            builder.HasIndex(p => p.MedicalRecordNumber).IsUnique();
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