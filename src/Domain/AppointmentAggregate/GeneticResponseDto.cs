using Newtonsoft.Json;

public class OperationRoomsDto
{
    [JsonProperty("room_id")]
    public string? Name { get; set; }
    [JsonProperty("date")]
    public string? Date { get; set; }
    [JsonProperty("agenda")]
    public List<OperationRoomAgendaDto>? Agenda { get; set; }

}


public class GeneticResponseDto
{

    [JsonProperty("room_agendas")]
    public List<OperationRoomsDto>? OperationRoomAgenda { get; set; }
}