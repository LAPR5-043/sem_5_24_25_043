using System;
using Domain.SurgeryRoomAggregate;
using src.Domain.Shared;
using Type = Domain.SurgeryRoomAggregate.Type;



namespace src.Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Represents a surgery room entity in the domain.
    /// </summary>
    public class SurgeryRoom : Entity<RoomNumber>, IAggregateRoot
    {
        /// <summary>
        /// Represents the ID of the surgery room.
        /// </summary>
        public RoomNumber roomNumber { get; private set; }
        /// <summary>
        /// Represents the type of the surgery room.
        /// </summary>
        public Type type { get; private set; }
        /// <summary>
        /// Represents the capacity of the surgery room.
        /// </summary>
        public Capacity capacity { get; private set; }
        /// <summary>
        /// Represents the current status of the surgery room.
        /// </summary>
        public CurrentStatus currentStatus { get; private set; }
        /// <summary>
        /// Represents the surgery room constructor.
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="type"></param>
        /// <param name="capacity"></param>
        /// <param name="currentStatus"></param>
        public SurgeryRoom(int roomNumber, string type, int capacity, string currentStatus)
        {
            this.roomNumber = new RoomNumber(Guid.NewGuid());
            this.type = TypeExtensions.FromString(type);
            this.capacity = new Capacity(capacity);
            this.currentStatus = CurrentStatusExtensions.FromString(currentStatus);
        }
        /// <summary>
        /// Represents the surgery room constructor.
        /// </summary>
        /// <param name="currentStatus"></param>
        public void ChangeCurrentStatus(string currentStatus)
        {
            this.currentStatus = CurrentStatusExtensions.FromString(currentStatus);
        }
        /// <summary>
        /// Represents the surgery room constructor.
        /// </summary>
        /// <param name="type"></param>
        public void ChangeType(string type)
        {
            this.type = TypeExtensions.FromString(type);
        }
        /// <summary>
        /// Represents the surgery room constructor.
        /// </summary>
        /// <param name="capacity"></param>
        public void ChangeCapacity(int capacity)
        {
            this.capacity = new Capacity(capacity);
        }
        /// <summary>
        /// Represents the surgery room constructor.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            SurgeryRoom other = (SurgeryRoom)obj;
            return roomNumber.Equals(other.roomNumber);
        }
        /// <summary>
        /// Represents the surgery room constructor.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return roomNumber.GetHashCode();
        }
    }
}
