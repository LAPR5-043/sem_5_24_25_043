using System;
using src.Domain.Shared;

namespace Domain.OperationRequestAggregate
{
    /// <summary>
    /// Value object representing the deadline date of an operation request.
    /// </summary>
    public class DeadlineDate : IValueObject
    {
        /// <summary>
        /// The deadline date of the operation request.
        /// </summary>
        public DateTime deadlineDate { get; }
        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        public DeadlineDate()
        {
            deadlineDate = DateTime.MinValue;
        }

        /// <summary>
        /// Constructor for creating a new instance of DeadlineDate from day, month, and year.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <exception cref="ArgumentException"></exception>
        public DeadlineDate(int day, int month, int year)
        {
            var date = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);

            if (date == DateTime.MinValue)
            {
                throw new ArgumentException("Deadline date cannot be empty");
            }

            this.deadlineDate = date;
        }


        public string Day()
        {
            return deadlineDate.Day.ToString();
        }

        public string Month()
        {
            return deadlineDate.Month.ToString();
        }

        public string Year()
        {
            return deadlineDate.Year.ToString();
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

            var other = (DeadlineDate)obj;
            return deadlineDate == other.deadlineDate;
        }
        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return deadlineDate.GetHashCode();
        }
        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
           
            return Day() + "/" + Month() + "/" + Year();
        }

        internal int CompareTo(DeadlineDate deadlineDate)
        {
            return this.deadlineDate.CompareTo(deadlineDate.deadlineDate);
        }
    }
}