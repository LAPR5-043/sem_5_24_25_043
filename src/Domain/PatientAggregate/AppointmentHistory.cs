using System;
using src.Domain.Shared;

namespace src.Domain.PatientAggregate
{
    /// <summary>
    /// Value object that represents the history of appointments of a patient.
    /// </summary>
    public class AppointmentHistory: IValueObject
    {
        /// <summary>
        /// List of appointment ids.
        /// </summary>
        private List<int> appointments { get; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AppointmentHistory()
        {
            appointments = new List<int>();
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

            this.appointments = appointments;
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
            return appointments == other.appointments;
        }
        /// <summary>
        /// Returns the hash code of the appointments list.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return appointments.GetHashCode();
        }
        /// <summary>
        /// Returns the string representation of the appointments list.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return appointments.ToString();
        }

        public List<int> Appointments()
        {
            return appointments;
        }

        /// <summary>
        /// Adds an appointment to the list.
        /// </summary>
        /// <param name="appointmentId"></param>
        public void AddAppointment(int appointmentId)
        {
            appointments.Add(appointmentId);
        }
        /// <summary>
        /// Removes an appointment from the list.
        /// </summary>
        /// <param name="appointmentId"></param>
        public void RemoveAppointment(int appointmentId)
        {
            appointments.Remove(appointmentId);
        }

    }
}