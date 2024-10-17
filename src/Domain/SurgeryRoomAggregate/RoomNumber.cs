using System;
using src.Domain.Shared;

namespace Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Represents the room number of a surgery room.
    /// </summary>
    public class RoomNumber : EntityId
    {
        /// <summary>
        /// The room number of the surgery room.
        /// </summary>
        public Guid number { get; }

        /// <summary>
        /// Creates a new instance of the RoomNumber class.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public RoomNumber(Guid value) : base(value)
        {
            number = value;
        }
        
        /// <summary>
        /// Compares two RoomNumber objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            RoomNumber other = (RoomNumber)obj;
            return number == other.number;
        }
        /// <summary>
        /// Returns the hash code of the RoomNumber object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return number.GetHashCode();
        }
        /// <summary>
        /// Returns the string representation of the RoomNumber object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string AsString()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates a RoomNumber object from a string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override object createFromString(string text)
        {
            throw new NotImplementedException();
        }
    }
}

