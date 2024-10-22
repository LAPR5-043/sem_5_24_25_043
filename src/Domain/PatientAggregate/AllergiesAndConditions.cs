using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Represents a value object for allergies and conditions.
    /// </summary>
    public class AllergiesAndConditions : IValueObject
    {
        /// <summary>
        /// Gets the allergies and conditions.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAggregate.AllergiesAndConditions"/> class with an empty value.
        /// </summary>
        public AllergiesAndConditions()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAggregate.AllergiesAndConditions"/> class with the specified value.
        /// </summary>
        /// <param name="allergiesAndConditions">The allergies and conditions.</param>
        /// <exception cref="ArgumentException">Thrown when the allergies and conditions are null or whitespace.</exception>
        public AllergiesAndConditions(string allergiesAndConditions)
        {
            if (string.IsNullOrWhiteSpace(allergiesAndConditions))
            {
                throw new ArgumentException("Allergies and conditions cannot be empty");
            }

            this.Value = allergiesAndConditions;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (AllergiesAndConditions)obj;
            return Value == other.Value;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}