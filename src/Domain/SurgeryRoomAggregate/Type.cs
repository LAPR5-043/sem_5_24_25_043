using System;

namespace Domain.SurgeryRoomAggregate
{
    /// <summary>
    /// Enum for the type of surgery room
    /// </summary>
    public enum Type
    {
        OperatingRoom,
        ConsultationRoom,
        RecoveryRoom
    }
    /// <summary>
    /// Extension methods for the Type enum
    /// </summary>
    public static class TypeExtensions
    {
        public static string ToFriendlyString(this Type type)
        {
            return type switch
            {
                Type.OperatingRoom => "Operating Room",
                Type.ConsultationRoom => "Consultation Room",
                Type.RecoveryRoom => "Recovery Room",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}