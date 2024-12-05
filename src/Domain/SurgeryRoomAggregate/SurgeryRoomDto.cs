using src.Domain.SurgeryRoomAggregate;

public class SurgeryRoomDto
    {
        public string? RoomID { get; set; }
        public string Name { get; set; }

        public SurgeryRoomDto()
        {
        }

        public SurgeryRoomDto(string roomID, string name)
        {
            RoomID = roomID;
            Name = name;
        }

        public SurgeryRoomDto(SurgeryRoom surgeryRoom)
        {
            RoomID = surgeryRoom.RoomID.Value;
            Name = surgeryRoom.Name;
        }
    }