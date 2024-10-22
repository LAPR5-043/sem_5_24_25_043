using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{   
    /// <summary>
    /// Value object representing the date of birth of a patient
    /// </summary>
    public class DateOfBirth : IValueObject
    {   
        /// <summary>
        /// Date of birth of the patient
        /// </summary>
        public DateTime Value { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DateOfBirth()
        {
            Value = DateTime.MinValue;
        }
        /// <summary>
        /// Constructor for the date of birth
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <exception cref="ArgumentException"></exception>
        public DateOfBirth(String day, String month, String year)
        {
            if (string.IsNullOrWhiteSpace(day) || string.IsNullOrWhiteSpace(month) || string.IsNullOrWhiteSpace(year))
            {
                throw new ArgumentException("Date of birth cannot be empty");
            }

            Value = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), 0, 0, 0, DateTimeKind.Utc);
        }
        /// <summary>
        /// Equality check for the date of birth
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (DateOfBirth)obj;
            return Value == other.Value;
        }
        /// <summary>
        /// Hash code for the date of birth
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// String representation of the date of birth
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        public string Day()
        {
            return Value.Day.ToString();
        }

        public string Month()
        {
            return Value.Month.ToString();
        }

        public string Year()
        {
            return Value.Year.ToString();
        }
        public static DateOfBirth FromString(string dateOfBirth)
        {
            string[] date = dateOfBirth.Split("/");
            return new DateOfBirth(date[0], date[1], date[2]);
        }
    }
}
    