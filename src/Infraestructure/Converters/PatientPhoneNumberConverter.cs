using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.PatientAggregate;

namespace Infrastructure.Converters
{
    public class PatientPhoneNumberConverter : ValueConverter<PatientPhoneNumber, string>
    {
        public PatientPhoneNumberConverter() : base(
            v => v.phoneNumber.ToString(),
            v => new PatientPhoneNumber(v))
        {
        }
    }
}