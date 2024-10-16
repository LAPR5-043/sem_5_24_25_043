using System.Text.RegularExpressions;
using src.Domain.Shared;

public class LicenseNumber : EntityId
{
    private static readonly string Pattern = @"^[NDO](19|20)\d{2}\d{5}$";
    private string LicenseNumberValue { get; }

    public LicenseNumber(string value) : base(value)
    {
        string valueStr = value.ToString();
        if (!Regex.IsMatch(valueStr, Pattern))
        {
            throw new ArgumentException("License number format is invalid.");
        }
        LicenseNumberValue = value;
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

    protected override object createFromString(string text)
    {
        return new LicenseNumber(text);
    }

    public override string AsString()
    {
        return LicenseNumberValue;
    }
}