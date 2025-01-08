using src.Domain.Shared;

/// <summary>
/// Value object representing the full name of a staff member.
/// </summary>
public class StaffFullName : IValueObject
{
    /// <summary>
    /// Full name of the staff member.
    /// </summary>
    public string fullName { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <exception cref="ArgumentException"></exception>
    public StaffFullName(StaffFirstName firstName, StaffLastName lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName.ToString()))
        {
            throw new ArgumentException("First name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(lastName.ToString()))
        {
            throw new ArgumentException("Last name cannot be empty");
        }

        fullName = firstName.ToString() + "," + lastName.ToString();
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public StaffFullName()
    {
    }

    /// <summary>
    /// Equality check.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffFullName staffFullName = (StaffFullName)obj;
        return fullName == staffFullName.fullName;
    }

    /// <summary>
    /// Hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return fullName.GetHashCode();
    }

    /// <summary>
    /// String representation.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return fullName;
    }

    public static implicit operator string?(StaffFullName? v)
    {
        throw new NotImplementedException();
    }
}
