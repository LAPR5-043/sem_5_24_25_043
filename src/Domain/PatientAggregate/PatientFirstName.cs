using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Value object representing the first name of a patient.
    /// </summary>
    public class PatientFirstName : IValueObject
    {
        public string firstName { get; }

        public PatientFirstName()
        {
            firstName = string.Empty;
        }

        public PatientFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty");
            }

            this.firstName = firstName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientFirstName)obj;
            return firstName == other.firstName;
        }

        public override int GetHashCode()
        {
            return firstName.GetHashCode();
        }

        public override string ToString()
        {
            return firstName;
        }
    }
}