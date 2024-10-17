using System;
using src.Domain.Shared;

namespace src.Domain.AppointmentAggregate
{
    /// <summary>
    /// Enumeration representing the status of an appointment.
    /// </summary>
    public enum Status
    {
        Scheduled,
        Cancelled,
        Completed
    }
    /// <summary>
    /// Extension methods for the Status enumeration.
    /// </summary>
    public static class StatusExtensions
    {
        /// <summary>
        /// Converts a string to a Status enumeration value.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Status FromString(string status)
        {
            return status.ToLower() switch
            {
                "scheduled" => Status.Scheduled,
                "cancelled" => Status.Cancelled,
                "completed" => Status.Completed,
                _ => throw new ArgumentException("Invalid status value")
            };
        }
    }
}