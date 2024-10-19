using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.PatientAggregate;

namespace Infrastructure.Converters
{
    public class MedicalRecordNumberConverter : ValueConverter<MedicalRecordNumber, string>
    {
        public MedicalRecordNumberConverter() : base(
            v => v.medicalRecordNumber,
            v => new MedicalRecordNumber(v))
        {
        }
    }
}