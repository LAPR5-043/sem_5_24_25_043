using System.Text.RegularExpressions;

public class StaffEmail
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[a-zA-Z0-9]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private string email { get; }

    public StaffEmail(string value)
    {
        if (!IsValidEmail(value))
        {
            throw new ArgumentException("Invalid email address format.", nameof(value));
        }
        email = value;
    }

    private static bool IsValidEmail(string email)
    {
        return EmailRegex.IsMatch(email);
    }

    public override string ToString()
    {
        return email;
    }  
}