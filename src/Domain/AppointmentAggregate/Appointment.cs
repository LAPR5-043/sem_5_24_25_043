using System;
using Domain.AppointmentAggregate;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.Shared;

namespace src.Domain.AppointmentAggregate
{
    /// <summary>
    /// Represents an appointment entity in the domain.
    /// </summary>
    public class Appointment : Entity<AppointmentID>, IAggregateRoot
    {
        /// <summary>
        /// Represents the ID of the appointment.
        /// </summary>
        public AppointmentID appointmentID { get; set; }
        /// <summary>
        /// Represents the ID of the request.
        /// </summary>
        public int requestID { get; set; }
        /// <summary>
        /// Represents the ID of the room.
        /// </summary>
        public string roomID { get; set; }
        /// <summary>
        /// Represents the date and time of the appointment.
        /// </summary>
        public DateAndTime dateAndTime { get; set; }
        /// <summary>
        /// Represents the status of the appointment.
        /// </summary>
        public Status status { get; set; }
        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="roomID"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="status"></param>
        /*public Appointment(string ID, int requestID, int roomID, int day, int month, int year, int hour, int minute, string status)
        {
            this.appointmentID = new AppointmentID(ID);
            this.requestID = requestID;
            this.roomID = roomID;
            this.dateAndTime = new DateAndTime(new DateTime(year, month, day, hour, minute, 0));
            this.status = StatusExtensions.FromString(status);
        }*/

        /// <summary>
        /// Change the status of the appointment.
        /// </summary>
        /// <param name="status"></param>
        public void ChangeStatus(string status)
        {
            this.status = StatusExtensions.FromString(status);
        }

        /// <summary>
        /// Change the date and time of the appointment.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public void ChangeDateAndTime(string day, string month, string year, string startT, string endT)
        {
            this.dateAndTime =new DateAndTime(year+month+day, startT, endT);
        }

        /// <summary>
        /// Change the room of the appointment.
        /// </summary>
        /// <param name="roomID"></param>
        public void ChangeRoom(string roomID)
        {
            this.roomID = roomID;
        }
        /// <summary>
        /// Override of the equality operator.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Appointment other = (Appointment)obj;
            return appointmentID.Equals(other.appointmentID);
        }
        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return appointmentID.GetHashCode();
        }

    }

}