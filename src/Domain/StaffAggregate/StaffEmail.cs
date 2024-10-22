using src.Domain.Shared;

/// <summary>
/// Represents the email of a staff member
/// </summary>
public class StaffEmail : IValueObject
{
    /// <summary>
    /// The email of the staff member
    /// </summary>
    public string email { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaffEmail"/> class.
    /// </summary>
    /// <param name="email"></param>
    /// <exception cref="ArgumentException"></exception>
    public StaffEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new System.ArgumentException("Email cannot be null or empty");
        }

        if (!email.Contains("@"))
        {
            throw new System.ArgumentException("Email must contain @");
        }

        var atIndex = email.IndexOf("@");
        if (atIndex == email.Length - 1 || !email.Substring(atIndex).Contains("."))
        {
            throw new ArgumentException("Email must contain a dot after @");
        }

        this.email = email;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaffEmail"/> class.
    /// </summary>
    public StaffEmail()
    {
        email = string.Empty;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffEmail staffEmail = (StaffEmail)obj;
        return email == staffEmail.email;

    }

    /// <summary>
    /// Serves as the default hash function
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return email.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return email;
    }

}