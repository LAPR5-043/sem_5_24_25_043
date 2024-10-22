using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Represents the Gender of a patient
    /// </summary>
    public enum Gender
    {
        Male,
        Female
    }

    public static class GenderExtensions
    {
        public static Gender FromString(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                throw new ArgumentException("Gender value cannot be null or empty");
            }

            gender = gender.Trim().ToLower();

            return gender switch
            {
                "male" => Gender.Male,
                "female" => Gender.Female,
                _ => throw new ArgumentException($"Invalid gender value: {gender}")
            };
        }
    }
}