using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{   
    /// <summary>
    /// Represents an emergency contact for a patient
    /// </summary>
    public class EmergencyContact : IValueObject
    {   
        /// <summary>
        /// The name of the emergency contact
        /// </summary>
        public string name { get; }
        /// <summary>
        /// The phone number of the emergency contact
        /// </summary>
        public string phoneNumber { get; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmergencyContact()
        {
            name = string.Empty;
            phoneNumber = string.Empty;
        }
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <exception cref="ArgumentException"></exception>
        public EmergencyContact(string name, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            if (phoneNumber.Length != 9)
            {
                throw new ArgumentException("Phone number must be 9 digits long");
            }

            this.name = name;
            this.phoneNumber = phoneNumber;
        }
        /// <summary>
        /// Compares two emergency contacts
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (EmergencyContact)obj;
            return name == other.name && phoneNumber == other.phoneNumber;
        }
        /// <summary>
        /// Generates a hash code for the emergency contact
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return name.GetHashCode() ^ phoneNumber.GetHashCode();
        }
        /// <summary>
        /// Returns a string representation of the emergency contact
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name + " " + phoneNumber;
        }
    }
}