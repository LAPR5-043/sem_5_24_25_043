using System.Text.RegularExpressions;
using src.Domain.Shared;

public class StaffPhoneNumber : IValueObject
{
    public string phoneNumber { get; }

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

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffPhoneNumber staffPhoneNumber = (StaffPhoneNumber)obj;
        return phoneNumber == staffPhoneNumber.phoneNumber;

    }
    public override int GetHashCode()
    {
        return phoneNumber.GetHashCode();
    }
    public override string ToString()
    {
        return phoneNumber;
    }

}