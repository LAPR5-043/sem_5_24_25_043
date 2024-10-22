using System;
using src.Domain.Shared;


namespace sem_5_24_25_043.Domain.AppointmentAggregate
{
    /// <summary>
    /// Value object representing the ID of an appointment.
    /// </summary>
    public class AppointmentID : EntityId 
    {
        public string ID { get; }

        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        /// <param name="ID"></param>
        public AppointmentID(string ID) : base(ID)
        {
            this.ID = ID;
        }

        /// <summary>
        /// Parameterless constructor required by Entity Framework.
        /// </summary>
        protected AppointmentID() : base(null)
        {
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

            var other = (AppointmentID)obj;
            return ID == other.ID;
        }
        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ID.ToString();
        }
        /// <summary>
        /// Override of the createFromString method.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override object createFromString(string text)
        {
            return new String(text);
        }

        public override string AsString()
        {
            return ID;
        }
    }
}
