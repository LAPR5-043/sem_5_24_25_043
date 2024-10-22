using Domain.PatientAggregate;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using AppContext = src.Models.AppContext;

public class MedicalRecordNumberGenerator : ValueGenerator<MedicalRecordNumber>
{
    public override MedicalRecordNumber Next(EntityEntry entry)
    {
        var context = (AppContext)entry.Context;

        var currentYearMonth = DateTime.Now.ToString("yyyyMM");

        var latestNumber = context.Patients
            .AsEnumerable()
            .Where(mr => mr.MedicalRecordNumber.ToString().StartsWith(currentYearMonth))
            .OrderByDescending(mr => mr.MedicalRecordNumber)
            .Select(mr => mr.MedicalRecordNumber)
            .FirstOrDefault();

        var sequentialNumber = latestNumber != null ? int.Parse(latestNumber.ToString().Substring(6)) + 1 : 1;

        var newNumber = $"{currentYearMonth}{sequentialNumber:D6}";

        MedicalRecordNumber medicalRecordNumber = new MedicalRecordNumber(newNumber);

        entry.Property("MedicalRecordNumber").CurrentValue = medicalRecordNumber;

        return medicalRecordNumber;
    }

    public override bool GeneratesTemporaryValues => false;
}