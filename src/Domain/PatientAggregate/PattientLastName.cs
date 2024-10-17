using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Value object representing the last name of a patient.
    /// </summary>
    public class PatientLastName : IValueObject
    {
        /// <summary>
        /// The last name of the patient.
        /// </summary>
        public string lastName { get; }
        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        public PatientLastName()
        {
            lastName = string.Empty;
        }
        /// <summary>
        /// Constructor for creating a new instance of PatientLastName.
        /// </summary>
        /// <param name="lastName"></param>
        /// <exception cref="ArgumentException"></exception>
        public PatientLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty");
            }

            this.lastName = lastName;
        }
        /// <summary>
        /// Override of the equality operator.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientLastName)obj;
            return lastName == other.lastName;
        }
        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return lastName.GetHashCode();
        }
        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return lastName;
        }
    }
}