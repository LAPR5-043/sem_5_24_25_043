using Newtonsoft.Json;

public class BookedSurgeryDto
{
    [JsonProperty("end")]
    public int End { get; set; }
    [JsonProperty("start")]
    public int Start { get; set; }
    [JsonProperty("surgery")]
    public int surgery { get; set; }
}

public class DoctorAgendaDto
{
    [JsonProperty("doctor_id")]
    public string DoctorId { get; set; }
    [JsonProperty("agenda")]
    public List<BookedSurgeryDto> Agenda { get; set; }
}

public class OperationRoomAgendaDto
{
    [JsonProperty("end")]
    public int End { get; set; }
    [JsonProperty("start")]
    public int Start { get; set; }
    [JsonProperty("surgery")]
    public int Surgery { get; set; }
}

public class PlanningResponseDto
{
    [JsonProperty("day")]
    public int Day { get; set; }
    [JsonProperty("doctor_agendas")]
    public List<DoctorAgendaDto> DoctorAgendas { get; set; }
    [JsonProperty("final_operation_time")]
    public int FinalOperationTime { get; set; }
    [JsonProperty("operation_room_agenda")]
    public List<OperationRoomAgendaDto> OperationRoomAgenda { get; set; }
    [JsonProperty("processing_time")]
    public double ProcessingTime { get; set; }
    [JsonProperty("room")]
    public string Room { get; set; }
}