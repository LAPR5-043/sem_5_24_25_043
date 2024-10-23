
using System.Text.RegularExpressions;
using src.Domain.Shared;

/// <summary>
/// Represents the license number of a staff member
/// </summary>
public class LicenseNumber : IValueObject
{
    /// <summary>
    /// The license number
    /// </summary>
    public string licenseNumber { get; }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="licenseNumber"></param>
    public LicenseNumber(string licenseNumber)
    {
        if (!Regex.IsMatch(licenseNumber, @"^$|^\d+$"))
        {
            throw new ArgumentException("Staff License number must be a number");
        }

        this.licenseNumber = licenseNumber;
    }

    public LicenseNumber()
    {
    }

    /// <summary>
    /// Compares two license numbers
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        LicenseNumber licenseNumber = (LicenseNumber)obj;
        return this.licenseNumber == licenseNumber.licenseNumber;
    }

    /// <summary>
    /// Generates a hash code for the license number
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return licenseNumber.GetHashCode();
    }

    /// <summary>
    /// Returns a string representation of the license number
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return licenseNumber;
    }

}