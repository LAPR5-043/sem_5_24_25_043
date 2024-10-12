public class StaffFullName
{
    private StaffFirstName firstName { get; }
    private StaffLastName lastName { get; }
    
    public StaffFullName(StaffFirstName firstName, StaffLastName lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public override string ToString()
    {
        return firstName.ToString() + " " + lastName.ToString();
    }    
}