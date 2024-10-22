using System.Text.RegularExpressions;
using src.Domain.Shared;

/// <summary>
/// Value object representing a staff member's phone number
/// </summary>
public class StaffPhoneNumber : IValueObject
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
    public StaffPhoneNumber(string phoneNumber)
    {
        if (!Regex.IsMatch(phoneNumber, @"^\+\d{1,3}\d{9,15}$"))
        {
            throw new ArgumentException("Staff Phone number is invalid");
        }

        this.phoneNumber = phoneNumber;
    }
    public StaffPhoneNumber()
    {

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

        StaffPhoneNumber staffPhoneNumber = (StaffPhoneNumber)obj;
        return phoneNumber == staffPhoneNumber.phoneNumber;

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