public class Staff
{
    
    private StaffFirstName firstName { get; set; }
    private StaffLastName lastName { get; set;}
    private StaffFullName fullName { get;set; }
    private LicenseNumber license { get;set; }
    private IsActive activeStatus { get;set; }
    private StaffContactInformation contactInformation { get;set; }
    private AvailabilitySlots availability { get; set;}
    
    public Staff(string firstName,string lastName, long license, bool isActive,
                 string staffPhoneNumber,string staffEmail, AvailabilitySlots availabilitySlots)
    {   
        this.firstName = new StaffFirstName(firstName);
        this.lastName = new StaffLastName(lastName);
        this.fullName = new StaffFullName(this.firstName, this.lastName);
        this.license = new LicenseNumber(license);
        activeStatus = new IsActive(isActive);
        contactInformation = new StaffContactInformation(staffEmail, staffPhoneNumber);
        availability = availabilitySlots;
    }
    
}