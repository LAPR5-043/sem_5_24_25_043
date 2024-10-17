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
        public static Type FromString(string type)
        {
            return type.ToLower() switch
            {
                "operatingroom" => Type.OperatingRoom,
                "consultationroom" => Type.ConsultationRoom,
                "recoveryroom" => Type.RecoveryRoom,
                _ => throw new ArgumentException("Invalid type value")

            };
        }
    }
}