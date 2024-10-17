using System;
using src.Domain.Shared;

namespace Domain.AppointmentAggregate
{
    /// <summary>
    /// Value object representing the date and time of an appointment.
    /// </summary>
    public class DateAndTime : IValueObject 
    {
        /// <summary>
        /// The date and time of the appointment.
        /// </summary>
        public DateTime Value { get; }
        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <exception cref="ArgumentException"></exception>
        public DateAndTime(int day, int month, int year, int hour, int minute)
        {
            var date = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);

            if (date == DateTime.MinValue)
            {
                throw new ArgumentException("Date and time cannot be empty");
            }

            this.Value = date;
        }
        /// <summary>
        /// Override of the equality operator.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (DateAndTime)obj;
            return Value == other.Value;
        }
        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}