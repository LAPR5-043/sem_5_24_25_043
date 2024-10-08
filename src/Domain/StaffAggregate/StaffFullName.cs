public class StaffFullName
{
    public StaffFirstName FirstName { get; }
    public StaffLastName LastName { get; }
    
    public StaffFullName(StaffFirstName firstName, StaffLastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}