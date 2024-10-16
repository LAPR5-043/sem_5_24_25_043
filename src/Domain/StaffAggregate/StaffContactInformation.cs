
public class StaffContactInformation
{
    public StaffEmail email { get; private set; }
    public StaffPhoneNumber phoneNumber { get; private set; }
    
    public StaffContactInformation(string email, string phoneNumber)
    {
        this.email = new StaffEmail(email);
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
    }

    public StaffContactInformation(){}

    public StaffEmail getEmail(){
        return email;
    }    

    public StaffPhoneNumber getPhoneNumber(){
        return phoneNumber;
    }    

    public void changeEmail(string email){
        this.email = new StaffEmail(email);
    }

    public void changePhoneNumber(string phoneNumber){
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
    }

}