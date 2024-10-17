using src.Domain.Shared;

public class StaffPhoneNumber : IValueObject
{
    public string phoneNumber { get; }

    public StaffPhoneNumber(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;
    }
    public StaffPhoneNumber()
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        StaffPhoneNumber staffPhoneNumber = (StaffPhoneNumber)obj;
        return phoneNumber == staffPhoneNumber.phoneNumber;

    }
    public override int GetHashCode() {
        return phoneNumber.GetHashCode();
    }
    public override string ToString()
    {
        return phoneNumber;
    }

}