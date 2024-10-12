using System;
using System.Text.RegularExpressions;

public class StaffFirstName
{
    private static readonly Regex NameRegex = new Regex(
        @"^[a-zA-Z]+$",
        RegexOptions.Compiled);

    private string name { get; }

    public StaffFirstName(string value)
    {
        if (!IsValidName(value))
        {
            throw new ArgumentException("Invalid first name format.", nameof(value));
        }
        name = value;
    }

    private static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && NameRegex.IsMatch(name);
    }

    public override string ToString()
    {
        return name;
    }
}