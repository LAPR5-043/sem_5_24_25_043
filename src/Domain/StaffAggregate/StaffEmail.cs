using src.Domain.Shared;

public class StaffEmail : IValueObject
{
    public string email { get; }

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
        
        this.email = email;
    }
    public StaffEmail()
    {
    
    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffEmail staffEmail = (StaffEmail)obj;
        return email == staffEmail.email;

    }
    public override int GetHashCode() {
        return email.GetHashCode();
    }
    public override string ToString()
    {
        return email;
    }

}