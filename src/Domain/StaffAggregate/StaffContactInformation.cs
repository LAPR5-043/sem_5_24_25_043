public class StaffContactInformation
{
    private StaffEmail email { get; set; }
    private StaffPhoneNumber phoneNumber { get; set; }
    
    public StaffContactInformation(string email, string phoneNumber)
    {
        this.email = new StaffEmail(email);
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
    }

    public StaffEmail getEmail(){
        return email;
    }    

    public StaffPhoneNumber getPhoneNumber(){
        return phoneNumber;
    }    

    public void setEmail(string email){
        this.email = new StaffEmail(email);
    }

    public void setPhoneNumber(string phoneNumber){
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
    }        
}