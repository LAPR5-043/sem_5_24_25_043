using System.Text.RegularExpressions;
using src.Domain.Shared;

public class LicenseNumber : EntityId
{
    public string LicenseNumberValue { get; private set; }

    public LicenseNumber(string value) : base(value)
    {
        if (!Regex.IsMatch(value, @"^[NDO](19|20)\d{2}\d{5}$"))
        {
            throw new ArgumentException("License number format is invalid.");
        }
        LicenseNumberValue = value;
    }

    protected LicenseNumber() : base(null)
    {
    }
      
    public override string ToString()
    {
        return LicenseNumberValue.ToString();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        LicenseNumber license = (LicenseNumber)obj;
        return LicenseNumberValue.Equals(license.LicenseNumberValue);
    }

    public override int GetHashCode()
    {
        return LicenseNumberValue.GetHashCode();
    }

    protected override Object createFromString(string text)
    {
        return text;
    }

    public override string AsString()
    {
        return LicenseNumberValue;
    }
}