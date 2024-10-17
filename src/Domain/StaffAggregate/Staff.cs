using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Shared;

public class Staff : Entity<LicenseNumber>, IAggregateRoot
{  
    public StaffFirstName firstName { get; private set; }

    public StaffLastName lastName { get; private set;}

    public StaffFullName fullName { get;private set; }

    public LicenseNumber StaffId { get;private set; }

    public IsActive activeStatus { get;private set; }

    public SpecializationName specializationId { get;private set; }
    
    public StaffContactInformation contactInformation { get;private set; }
    public AvailabilitySlots availability { get;private set;}

    
    public Staff(string firstName,string lastName, string license, bool isActive,
                 string staffPhoneNumber,string staffEmail,string specializationName, AvailabilitySlots availabilitySlots)
    {   
        this.firstName = new StaffFirstName(firstName);
        this.lastName = new StaffLastName(lastName);
        this.fullName = new StaffFullName(this.firstName, this.lastName);
        this.StaffId = new LicenseNumber(license);
        this.specializationId = new SpecializationName(specializationName);
        activeStatus = new IsActive(isActive);
        contactInformation = new StaffContactInformation(staffEmail, staffPhoneNumber);
        availability = availabilitySlots;
    }
    public Staff(){
        this.StaffId = null;      
        
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
        return StaffId.ToString();
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
        this.StaffId = new LicenseNumber(license);
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