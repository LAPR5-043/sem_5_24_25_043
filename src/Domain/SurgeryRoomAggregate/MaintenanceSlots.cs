using System;
using src.Domain.Shared;

namespace Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Represents the maintenance slots of a surgery room.
    /// </summary>
    public class MaintenanceSlots : IValueObject 
    {
        /// <summary>
        /// The start of the maintenance slot.
        /// </summary>
        public DateTime start { get; }
        public DateTime end { get; }

        public MaintenanceSlots(int day, int month, int year, int startHour, int startMinute, int endHour, int endMinute)
        {
            start = new DateTime(year, month, day, startHour, startMinute, 0, DateTimeKind.Local);
            end = new DateTime(year, month, day, endHour, endMinute, 0, DateTimeKind.Local);
        }
    
    }
}        