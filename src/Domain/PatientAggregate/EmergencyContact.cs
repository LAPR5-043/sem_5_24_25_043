using System;
using System.Text.RegularExpressions;
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
        public string Name { get; }
        /// <summary>
        /// The phone number of the emergency contact
        /// </summary>
        public string PhoneNumber { get; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmergencyContact()
        {
            Name = string.Empty;
            PhoneNumber = string.Empty;
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

            if (!Regex.IsMatch(phoneNumber, @"^\+\d{1,3}\d{9,15}$"))
            {
                throw new ArgumentException("Emergency contact phone number is invalid");
            }


            this.Name = name;
            this.PhoneNumber = phoneNumber;
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
            return Name == other.Name && PhoneNumber == other.PhoneNumber;
        }
        /// <summary>
        /// Generates a hash code for the emergency contact
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ PhoneNumber.GetHashCode();
        }
        /// <summary>
        /// Returns a string representation of the emergency contact
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + " " + PhoneNumber;
        }
    }
}