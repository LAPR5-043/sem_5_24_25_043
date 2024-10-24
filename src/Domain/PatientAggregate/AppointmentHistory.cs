using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Value object that represents the history of appointments of a patient.
    /// </summary>
    public class AppointmentHistory: IValueObject
    {
        /// <summary>
        /// List of appointment ids.
        /// </summary>
        private List<int> Value { get; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AppointmentHistory()
        {
            Value = new List<int>();
        }
        /// <summary>
        /// Constructor that initializes the list of appointments.
        /// </summary>
        /// <param name="appointments"></param>
        /// <exception cref="ArgumentException"></exception>
        public AppointmentHistory(List<int> appointments)
        {
            if (appointments == null)
            {
                throw new ArgumentException("Appointments cannot be null");
            }

            this.Value = appointments;
        }
        /// <summary>
        /// Compares two instances of AppointmentHistory.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (AppointmentHistory)obj;
            return Value == other.Value;
        }
        /// <summary>
        /// Returns the hash code of the appointments list.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// Returns the string representation of the appointments list.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value?.ToString() ?? string.Empty;
        }

        public List<int> Appointments()
        {
            return Value;
        }

        /// <summary>
        /// Adds an appointment to the list.
        /// </summary>
        /// <param name="appointmentId"></param>
        public void AddAppointment(int appointmentId)
        {
            Value.Add(appointmentId);
        }
        /// <summary>
        /// Removes an appointment from the list.
        /// </summary>
        /// <param name="appointmentId"></param>
        public void RemoveAppointment(int appointmentId)
        {
            Value.Remove(appointmentId);
        }

    }
}