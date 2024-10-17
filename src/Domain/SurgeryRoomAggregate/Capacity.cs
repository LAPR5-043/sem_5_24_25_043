using System;
using src.Domain.Shared;

namespace Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Represents the capacity of a surgery room.
    /// </summary>
    public class Capacity : IValueObject
    {   
        /// <summary>
        /// The capacity of the surgery room.
        /// </summary>
        public int roomCapacity { get; }

        /// <summary>
        /// Creates a new instance of the Capacity class.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public Capacity(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0");
            }

            roomCapacity = value;
        }
        /// <summary>
        /// Compares two Capacity objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
            return false;
            }

            Capacity other = (Capacity)obj;
            return roomCapacity == other.roomCapacity;
        }
        /// <summary>
        /// Returns the hash code of the Capacity object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return roomCapacity.GetHashCode();
        }

    }
}