 
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Schedule
{
    public class ScheduleDto
    {
        [JsonProperty("agenda_operation_room")]
        public List<OperationRoom> AgendaOperationRoom { get; set; }
        [JsonProperty("timetables")]
        public List<TimetableDto> Timetables { get; set; }
        [JsonProperty("staff")]
        public List<StaffDesc> Staff { get; set; }
        [JsonProperty("assignment_surgery")]
        public List<AssignmentSurgeryDto> AssignmentSurgery { get; set; }
        [JsonProperty("surgery_id")]
        public List<SurgeryIdDto> SurgeryId { get; set; }
        [JsonProperty("agenda_staff")]
        public List<AgendaStaffDto> AgendaStaff { get; set; }
        [JsonProperty("surgery")]
        public List<SurgeryDto> Surgery { get; set; }

        public ScheduleDto()
        {
            Timetables = new List<TimetableDto>();
            Staff = new List<StaffDesc>();
            AssignmentSurgery = new List<AssignmentSurgeryDto>();
            SurgeryId = new List<SurgeryIdDto>();
            AgendaStaff = new List<AgendaStaffDto>();
            Surgery = new List<SurgeryDto>();
            AgendaOperationRoom = new List<OperationRoom>();
            
        }
    }

    public class TimetableDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }

    }

    public class StaffDesc
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("specialty")]
        public string Specialty { get; set; }
        [JsonProperty("operations")]
        public List<string> Operations { get; set; }
    }

    public class AssignmentSurgeryDto
    {
        [JsonProperty("surgery_id")]
        public string SurgeryId { get; set; }
        [JsonProperty("doctor_id")]
        public string DoctorId { get; set; }
    }

    public class SurgeryIdDto
    {
        [JsonProperty("surgery_id")]
        public string SurgeryId { get; set; }
        [JsonProperty("surgery_type")]
        public string SurgeryType { get; set; }
    }

    public class AgendaStaffDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("agenda")]
        public string Agenda { get; set; }
    }

    public class SurgeryDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("cleaning")]
        public string Cleaning { get; set; }
    }

    public class OperationRoom
    {
        [JsonProperty("room_id")]
        public string room_id { get; set; }
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("agenda")]
        public string agenda { get; set; }
    }


}