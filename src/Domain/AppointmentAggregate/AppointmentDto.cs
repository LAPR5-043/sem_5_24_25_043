using System.Text.Json.Serialization;
using Domain.OperationRequestAggregate;

namespace src.Domain.AppointmentAggregate
{
    public class AppointmentDto
    {
        /// <summary>
        /// Represents the ID of the appointment.
        /// </summary>
        public string? AppointmentID { get; set; }

        /// <summary>
        /// Represents the ID of the request.
        /// </summary>
        public OperationRequestDto? Request { get; set; }

        /// <summary>
        /// Represents the ID of the room.
        /// </summary>
        public string? RoomID { get; set; }

        /// <summary>
        /// Represents the date and time of the appointment.
        /// </summary>
        public DateAndTimeDto? DateAndTime { get; set; }

        /// <summary>
        /// Represents the status of the appointment.
        /// </summary>
        public string?  Status{ get; set; }
   
        public AppointmentDto(Appointment appointment)
        {
            AppointmentID = appointment.appointmentID.ToString();
        
            RoomID = appointment.roomID;
            DateAndTime = new DateAndTimeDto
            {
                StartT = appointment.dateAndTime.startT,
                EndT = appointment.dateAndTime.endT,
                Date = appointment.dateAndTime.date
            };
            Status = appointment.status.ToString();
            Request = new OperationRequestDto();
        } 
    [JsonConstructor]
    public AppointmentDto(string appointmentID, string roomID, DateAndTimeDto dateAndTime, string status, OperationRequestDto request)
    {
        AppointmentID = appointmentID;
        
        RoomID = roomID;
        DateAndTime = dateAndTime;
        Status = status;
        Request = request;
    }
        
        
        }
    public class DateAndTimeDto
    {
        public string StartT { get; set; }
        public string EndT { get; set; }
        public string Date { get; set; }
    }
}