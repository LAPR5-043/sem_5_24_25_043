using src.Domain.Shared;

/// <summary>
/// Value object representing the first name of a staff member.
/// </summary>
public class StaffFirstName : IValueObject
{

    /// <summary>
    /// The first name of the staff member
    /// </summary>
    public string firstName { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="firstName"></param>
    /// <exception cref="ArgumentException"></exception>
    public StaffFirstName(string firstName)
    {

        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First Name cannot be null or empty");
        }
        if (!firstName.All(char.IsLetter))
        {
            throw new ArgumentException("First Name must contain only letters");
        }
        this.firstName = firstName;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public StaffFirstName()
    {
        firstName = string.Empty;
    }

    /// <summary>
    /// Compares two first names for equality
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffFirstName staffFirstName = (StaffFirstName)obj;
        return firstName == staffFirstName.firstName;

    }

    /// <summary>
    /// Returns the hash code for the first name
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return firstName.GetHashCode();
    }

    /// <summary>
    /// Returns the string representation of the first name
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return firstName;
    }

}