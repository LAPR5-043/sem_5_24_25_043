using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{   
    /// <summary>
    /// Value object representing the full name of a patient.
    /// </summary>
    public class PatientFullName : IValueObject
    {
        /// <summary>
        /// Full name of the patient.
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PatientFullName()
        {
            Value = string.Empty;
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <exception cref="ArgumentException"></exception>
        public PatientFullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty");
            }

            Value = firstName + " " + lastName;
        }
        /// <summary>
        /// Equality check.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientFullName)obj;
            return Value == other.Value;
        }

        /// <summary>
        /// Hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}