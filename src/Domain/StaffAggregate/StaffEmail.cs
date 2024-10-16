using System.Text.RegularExpressions;

public class StaffEmail
{


    public string email { get; }

    public StaffEmail(string value)
    {
        if (!IsValidEmail(value))
        {
            throw new ArgumentException("Invalid email address format.", nameof(value));
        }
        email = value;
    }

    public StaffEmail() { }

    private static bool IsValidEmail(string email)
    {
        return new Regex(@"^[a-zA-Z0-9]+@[^@\s]+\.[^@\s]+$").IsMatch(email);
    }

    public override string ToString()
    {
        return email;
    }  
}