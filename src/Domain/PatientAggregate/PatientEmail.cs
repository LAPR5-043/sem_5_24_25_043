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
        public string email { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientEmail"/> class.
        /// </summary>
        public PatientEmail()
        {
            email = string.Empty;
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

            this.email = email;
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientEmail)obj;
            return email == other.email;
        }
        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return email.GetHashCode();
        }
        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return email;
        }
    }
}