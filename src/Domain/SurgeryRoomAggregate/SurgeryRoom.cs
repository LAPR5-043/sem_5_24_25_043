using System;
using Domain.SurgeryRoomAggregate;
using src.Domain.Shared;




namespace src.Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Represents a surgery room entity in the domain.
    /// </summary>
    public class SurgeryRoom : Entity<RoomId>, IAggregateRoot
    {
        public RoomId RoomID { get; set; }
        public string Name { get; set; }


        public SurgeryRoom(string roomID, string name)
        {
            RoomID = new RoomId(roomID);
            Name = name;

        }

        public SurgeryRoom()
        {
        }

        public override bool Equals(Object other)
        {
            if (other == null || other.GetType() != this.GetType())
            {
                return false;
            }

            SurgeryRoom otherRoom = (SurgeryRoom)other;
            return this.RoomID == otherRoom.RoomID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoomID);
        }
    }
}
