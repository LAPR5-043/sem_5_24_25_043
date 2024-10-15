using System;
using System.Text.RegularExpressions;

public class StaffPhoneNumber
{
    private static readonly Regex PhoneNumberRegex = new Regex(
        @"^9\d{8}$",
        RegexOptions.Compiled);

    private string phoneNumber { get; }

    public StaffPhoneNumber(string value)
    {
        if (!isValidPhoneNumber(value))
        {
            throw new ArgumentException("Invalid phone number format. It must start with 9 and have 9 digits.", nameof(value));
        }
        phoneNumber = value;
    }

    private static bool isValidPhoneNumber(string phoneNumber)
    {
        return PhoneNumberRegex.IsMatch(phoneNumber);
    }

    public override string ToString()
    {
        return phoneNumber;
    }
}