public class StaffContactInformation
{
    public StaffEmail Email { get; }
    public StaffPhoneNumber PhoneNumber { get; }
    
    public StaffContactInformation(StaffEmail email, StaffPhoneNumber phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }
}