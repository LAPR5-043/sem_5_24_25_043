public class StaffFullName
{
    public StaffFirstName firstName { get; }
    public StaffLastName lastName { get; }
    
    public StaffFullName(StaffFirstName firstName, StaffLastName lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public StaffFullName() {}   
    public override string ToString()
    {
        return firstName.ToString() + " " + lastName.ToString();
    }    
}