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
        public static string ToFriendlyString(this CurrentStatus status)
        {
            return status switch
            {
                CurrentStatus.Available => "Available",
                CurrentStatus.Occupied => "Occupied",
                CurrentStatus.UnderMaintenance => "Under Maintenance",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}