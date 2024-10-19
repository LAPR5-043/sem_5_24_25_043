using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Converters
{
    public class DateTimeConverter : ValueConverter<DateTime, string>
    {
        public DateTimeConverter() : base(
            v => v.ToString("o"), 
            v => DateTime.Parse(v)) 
        {
        }
    }
}