using src.Domain.Shared;


/// <summary>
/// Value object representing the last name of a staff member.
/// </summary>
public class StaffLastName : IValueObject
{
    /// <summary>
    /// The last name of the staff member.
    /// </summary>
    public string lastName { get; }

    /// <summary>
    /// Constructor for creating a new instance of StaffLastName.
    /// </summary>
    /// <param name="lastName"></param>
    /// <exception cref="ArgumentException"></exception>
    public StaffLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new System.ArgumentException("First Name cannot be null or empty");
        }
        if (!lastName.All(char.IsLetter))
        {
            throw new System.ArgumentException("First Name must contain only letters");
        }
        this.lastName = lastName;
    }

    /// <summary>
    /// Default constructor required by Entity Framework.
    /// </summary>
    public StaffLastName()
    {

    }

    /// <summary>
    /// Override of the equality operator.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffLastName staffFirstName = (StaffLastName)obj;
        return lastName == staffFirstName.lastName;

    }

    /// <summary>
    /// Override of the GetHashCode method.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return lastName.GetHashCode();
    }

    /// <summary>
    /// Override of the ToString method.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return lastName;
    }
}