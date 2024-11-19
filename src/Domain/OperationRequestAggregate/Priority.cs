using System;
using src.Domain.Shared;

namespace src.Domain.OperationRequestAggregate
{
    /// <summary>
    /// Enumeration representing the priority of an operation request.
    /// </summary>
    public enum Priority
    {
        Effective,
        Urgent,
        Emergency
    }

    
    /// <summary>
    /// Extension methods for the Priority enumeration.
    /// </summary>
    public static class PriorityExtensions
    {
        public static Priority FromString(string priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
            {
                throw new ArgumentException("Priority cannot be null or empty");
            }

            return priority.ToLower() switch
            {
                "effective" => Priority.Effective,
                "urgent" => Priority.Urgent,
                "emergency" => Priority.Emergency,
                _ => throw new ArgumentException("Invalid priority value")
            };
        }
        public static int ToInt(this Priority priority)
        {
            return priority switch
            {
                Priority.Effective => 0,
                Priority.Urgent => 1,
                Priority.Emergency => 2,
                _ => throw new ArgumentException("Invalid priority value")
            };
        }
    }
}