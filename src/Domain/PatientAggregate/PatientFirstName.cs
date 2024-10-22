using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Value object representing the first name of a patient.
    /// </summary>
    public class PatientFirstName : IValueObject
    {   
        /// <summary>
        /// The first name of the patient
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientFirstName()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName"></param>
        /// <exception cref="ArgumentException"></exception>
        public PatientFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty");
            }

            this.Value = firstName;
        }

        /// <summary>
        /// Compares two first names for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientFirstName)obj;
            return Value == other.Value;
        }
        
        /// <summary>
        /// Returns the hash code for the first name
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the first name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}