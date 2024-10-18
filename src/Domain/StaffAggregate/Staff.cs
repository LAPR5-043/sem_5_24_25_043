using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.Shared;

public class Staff : Entity<StaffID>, IAggregateRoot {

    public StaffID staffID { get; set; }
    //[Required]
    public StaffFirstName firstName { get;  set; }
    //[Required]
    public StaffLastName lastName { get;  set;}
   // [Required]
    public StaffFullName fullName { get;  set;}
    //[Required]
    public StaffEmail email { get;  set;}
    //[Required]
    public StaffPhoneNumber phoneNumber { get;  set;}
    //[Required]
    public LicenseNumber licenseNumber { get;  set;}
    //[Required]
    public bool isActive { get;  set; }
    //[Required]    
    public AvailabilitySlots availabilitySlots { get; set; }
    //[Required]
    //[ForeignKey("Specialization")]
    public string specializationID { get;  set; }

    /*public Staff(string staffID, string firstName, string lastName, string email, 
                string phoneNumber, string licenseNumber, bool isActive,
                 string specializationID){
        this.staffID = new StaffID(staffID);
        this.firstName = new StaffFirstName(firstName);
        this.lastName =  new StaffLastName(lastName);
        this.fullName = new StaffFullName(this.firstName, this.lastName);
        this.email = new StaffEmail(email);
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
        this.licenseNumber = new LicenseNumber(licenseNumber);
        this.isActive = isActive;
        this.availabilitySlots = new AvailabilitySlots();
        this.specializationID = specializationID;
    }

    public Staff(StaffID staffID, StaffFirstName firstName, StaffLastName lastName,
                StaffEmail email, StaffPhoneNumber phoneNumber, LicenseNumber licenseNumber,
                bool isActive, AvailabilitySlots availabilitySlots, string specializationID){
        this.staffID = staffID;
        this.firstName = firstName;
        this.lastName = lastName;
        this.fullName = new StaffFullName(this.firstName, this.lastName);
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.licenseNumber = licenseNumber;
        this.isActive = isActive;
        this.availabilitySlots = availabilitySlots;
        this.specializationID = specializationID;
    }*/

    public void changeFirstName(string firstName){
        this.firstName = new StaffFirstName(firstName);
    }

    public void changeLastName(string lastName){
        this.lastName = new StaffLastName(lastName);
    }

    public void changeEmail(string email){
        this.email = new StaffEmail(email);
    }

    public void changePhoneNumber(string phoneNumber){
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
    }

    public void changeLicenseNumber(string licenseNumber){
        this.licenseNumber = new LicenseNumber(licenseNumber);
    }

    public void changeActiveStatus(bool isActive){
        this.isActive = isActive;
    }

    public void changeAvailabilitySlots(AvailabilitySlots availabilitySlots){
        this.availabilitySlots = availabilitySlots;
    }

    public void changeFullName(StaffFullName fullName){
        this.fullName = fullName;
    }

    public override bool Equals(object obj){
        if(obj == null || GetType() != obj.GetType()){
            return false;
        }

        Staff staff = (Staff)obj;
        return staffID == staff.staffID;
    }

    public override int GetHashCode() {
        return staffID.GetHashCode();
    }

    public override string ToString(){
        return staffID.ToString();
    }
}





