using System;


namespace Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Enum for the current status of the surgery room
    /// </summary>
    public enum CurrentStatus
    {
        Available,
        Occupied,
        UnderMaintenance
    }
    /// <summary>
    /// Extension methods for the CurrentStatus enum
    /// </summary>
    public static class CurrentStatusExtensions
    {
        public static CurrentStatus FromString(string status)
        {
            return status.ToLower() switch
            {
                "available" => CurrentStatus.Available,
                "occupied" => CurrentStatus.Occupied,
                "undermaintenance" => CurrentStatus.UnderMaintenance,
                _ => throw new ArgumentException("Invalid status value")

            };
        }
    }
}
