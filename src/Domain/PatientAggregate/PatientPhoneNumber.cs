using System;
using System.Text.RegularExpressions;
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
        public string Value { get; }
        /// <summary>
        /// Constructor for the PatientPhoneNumber class
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <exception cref="ArgumentException"></exception>
        public PatientPhoneNumber(string phoneNumber)
        {

            Console.WriteLine($"Validating phone number: '{phoneNumber}'");

            if (!Regex.IsMatch(phoneNumber, @"^\+\d{1,3}\d{9,15}$"))
            {
                throw new ArgumentException("Phone number is invalid");
            }

            this.Value = phoneNumber;
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
            return Value == other.Value;
        }
        /// <summary>
        /// Overriding the GetHashCode method to return the hash code of the phone number
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// Overriding the ToString method to return the phone number as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }

    }
}