using src.Domain.Shared;

public class StaffPhoneNumber : IValueObject
{
    public string phoneNumber { get; }

    public StaffPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new System.ArgumentException("Phone Number cannot be null or empty");
        }
        if(phoneNumber.Length == 9 && phoneNumber[0] == '9' && phoneNumber.All(char.IsDigit))
        {
            throw new System.ArgumentException("Phone Number must be 10 digits");
        }
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