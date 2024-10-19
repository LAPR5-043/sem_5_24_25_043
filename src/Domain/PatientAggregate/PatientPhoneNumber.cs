using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{   
    /// <summary>
    /// Value object representing a patient's phone number
    /// </summary>
    public class PatientPhoneNumber : IValueObject
    {   
        /// <summary>
        /// The phone number
        /// </summary>
        public string phoneNumber { get; }
        /// <summary>
        /// Constructor for the PatientPhoneNumber class
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <exception cref="ArgumentException"></exception>
        public PatientPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 9)
            {
                throw new ArgumentException("Phone number must be 9 digits long");
            }

            this.phoneNumber = phoneNumber;
        }
        /// <summary>
        /// Overriding the Equals method to compare two PatientPhoneNumber objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (PatientPhoneNumber)obj;
            return phoneNumber == other.phoneNumber;
        }
        /// <summary>
        /// Overriding the GetHashCode method to return the hash code of the phone number
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return phoneNumber.GetHashCode();
        }
        /// <summary>
        /// Overriding the ToString method to return the phone number as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return phoneNumber;
        }
    }
}