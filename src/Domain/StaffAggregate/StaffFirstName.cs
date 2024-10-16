using System;
using System.Text.RegularExpressions;

public class StaffFirstName
{
    public string name { get; private set;}

    public StaffFirstName(string value)
    {
        if (!IsValidName(value))
        {
            throw new ArgumentException("Invalid first name format.", nameof(value));
        }
        name = value;
    }

    public StaffFirstName() { }

    private static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && new Regex(@"^[a-zA-Z]+$").IsMatch(name);
    }

    public override string ToString()
    {
        return name;
    }
}