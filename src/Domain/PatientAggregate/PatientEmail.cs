using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    /// <summary>
    /// Represents the email of a patient
    /// </summary>
    public class PatientEmail : IValueObject
    {   
        /// <summary>
        /// The email of the patient
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientEmail"/> class.
        /// </summary>
        public PatientEmail()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientEmail"/> class.
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="ArgumentException"></exception>
        public PatientEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty");
            }

            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email must contain @");
            }
            
            var atIndex = email.IndexOf("@");
            if (atIndex == email.Length - 1 || !email.Substring(atIndex).Contains("."))
            {
                throw new ArgumentException("Email must contain a dot after @");
            }

            this.Value = email;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientEmail)obj;
            return Value == other.Value;
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        
        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}