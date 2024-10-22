using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using Infrastructure.Converters;
using Microsoft.CodeAnalysis.Elfie.Model;
using Domain.OperationRequestAggregate;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;

public class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest>
    {
        public void Configure(EntityTypeBuilder<OperationRequest> builder)
        {
            builder.ToTable("OperationRequests");

            builder.HasKey(p => p.operationRequestID);

            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new OperationRequestID(v)
                )
                .HasValueGenerator<OperationRequestIDGenerator>()
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.operationRequestID)
                .HasConversion(
                    v => v.AsString(),
                    v => new OperationRequestID(v)
                )
                .IsRequired();
                

            builder.Property(t => t.patientID)
                .IsRequired();

            builder.Property(t => t.doctorID)
                .IsRequired();

            builder.Property(t => t.operationTypeID)
                .IsRequired();

            builder.Property(t => t.deadlineDate)
                .HasConversion(
                    v => v.ToString(),
                    v => parseDeadlineDate(v)
                )
                .IsRequired();

            builder.Property(t => t.priority)
                .HasConversion(
                    v => v.ToString(),
                    v => PriorityExtensions.FromString(v)
                )
                .IsRequired();      
        }
        private DeadlineDate parseDeadlineDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out var date))
            {
                return new DeadlineDate(date.Day, date.Month, date.Year);
            }
            throw new ArgumentException("Invalid date format");
        }

    }
