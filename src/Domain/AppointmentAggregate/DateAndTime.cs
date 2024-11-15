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
        public string date { get; }
        public string startT {get;}
        public string endT {get;}

        // Parameterless constructor for EF Core
        private DateAndTime() { }
 
        public DateAndTime(string  date, string startT, string endT)
        {
            this.date = date;
            this.startT = startT;
            this.endT = endT;
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
            return date == other.date && startT == other.startT && endT == other.endT;
        }

        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(date, startT, endT);
        }

        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        public override string ToString()
        {
            return $"{date},{startT},{endT}";
        }
    }
}