using src.Domain.Shared;

public class StaffEmail : IValueObject
{
    public string email { get; }

    public StaffEmail(string email)
    {
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