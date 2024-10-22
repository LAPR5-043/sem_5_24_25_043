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

        // Parameterless constructor for EF Core
        private DateAndTime() { }

        public DateAndTime(DateTime value)
        {
            Value = value;
        }

        /// <summary>
        /// Override of the equality operator.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (DateAndTime)obj;
            return Value.Equals(other.Value);
        }

        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}