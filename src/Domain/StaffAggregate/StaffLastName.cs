using System;
using System.Text.RegularExpressions;

public class StaffLastName
{
    private static readonly Regex nameRegex = new Regex(
        @"^[a-zA-Z]+$",
        RegexOptions.Compiled);

    public string Value { get; }

    public StaffLastName(string value)
    {
        if (!isValidName(value))
        {
            throw new ArgumentException("Invalid last name format.", nameof(value));
        }

        Value = value;
    }

    public StaffLastName() { }

    private static bool isValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && nameRegex.IsMatch(name);
    }

    public override string ToString()
    {
        return Value;
    }
}