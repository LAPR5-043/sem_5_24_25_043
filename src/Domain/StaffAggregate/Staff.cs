using src.Domain.Shared;

public class Staff : Entity<LicenseNumber>, IAggregateRoot
{
    
    private StaffFirstName firstName { get; set; }
    private StaffLastName lastName { get; set;}
    private StaffFullName fullName { get;set; }
    private LicenseNumber license { get;set; }
    private IsActive activeStatus { get;set; }
    private SpecializationName specializationId { get;set; }
    private StaffContactInformation contactInformation { get;set; }
    private AvailabilitySlots availability { get; set;}

    
    public Staff(string firstName,string lastName, string license, bool isActive,
                 string staffPhoneNumber,string staffEmail,string specializationName, AvailabilitySlots availabilitySlots)
    {   
        this.firstName = new StaffFirstName(firstName);
        this.lastName = new StaffLastName(lastName);
        this.fullName = new StaffFullName(this.firstName, this.lastName);
        this.license = new LicenseNumber(license);
        this.specializationId = new SpecializationName(specializationName);
        activeStatus = new IsActive(isActive);
        contactInformation = new StaffContactInformation(staffEmail, staffPhoneNumber);
        availability = availabilitySlots;
    }

    public string FirstName(){
        return firstName.ToString();
    }   
    
    public string LastName(){
        return lastName.ToString();
    }

    public string FullName(){
        return fullName.ToString();
    }

    public string License(){
        return license.ToString();
    }

    public bool IsActive(){
        return activeStatus.isActive();
    }

    public string PhoneNumber(){
        return contactInformation.getPhoneNumber().ToString();
    }

    public string Email(){
        return contactInformation.getEmail().ToString();
    }

    public string Specialization(){
        return specializationId.ToString();
    }

    public AvailabilitySlots Availability(){
        return availability;
    }

    public void changeFirstName(string firstName){
        this.firstName = new StaffFirstName(firstName);
    }

    public void changeLastName(string lastName){
        this.lastName = new StaffLastName(lastName);
    }

    public void changeLicense(string license){
        this.license = new LicenseNumber(license);
    }

    public void changeActiveStatus(bool isActive){
        activeStatus = new IsActive(isActive);
    }

    public void changePhoneNumber(string phoneNumber){
        contactInformation.changePhoneNumber(phoneNumber);
    }
    
    public void changeEmail(string email){
        contactInformation.changeEmail(email);
    }

    public void changeSpecialization(string specializationName){
        this.specializationId = new SpecializationName(specializationName);
    }

    public void changeAvailability(AvailabilitySlots availabilitySlots){
        availability = availabilitySlots;
    }


}