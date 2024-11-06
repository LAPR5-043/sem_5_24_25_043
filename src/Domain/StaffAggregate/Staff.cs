using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Domain.AvailabilitySlotAggregate;
using src.Domain.Shared;

public class Staff : Entity<StaffID>, IAggregateRoot
{

    public StaffID staffID { get; set; }
    //[Required]
    public StaffFirstName firstName { get; set; }
    //[Required]
    public StaffLastName lastName { get; set; }
    // [Required]
    public StaffFullName fullName { get; set; }
    //[Required]
    public StaffEmail email { get; set; }
    //[Required]
    public StaffPhoneNumber phoneNumber { get; set; }
    //[Required]
    public LicenseNumber licenseNumber { get; set; }
    //[Required]
    public bool isActive { get; set; }
    //[Required]    
    public string availabilitySlotsID { get; set; }
    //[Required]
    //[ForeignKey("Specialization")]
    public string specializationID { get; set; }

    /*public Staff(string staffID, string firstName, string lastName, string email, 
                string phoneNumber, string licenseNumber, bool isActive,
                 string specializationID){
        this.staffID = new StaffID(staffID);
        this.firstName = new StaffFirstName(firstName);
        this.lastName = new StaffLastName(lastName);
        this.fullName = new StaffFullName(this.firstName, this.lastName);
        this.email = new StaffEmail(email);
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
        this.licenseNumber = new LicenseNumber(licenseNumber);
        this.isActive = isActive;
        this.availabilitySlots = new AvailabilitySlots();
        this.specializationID = specializationID;
    }

    public Staff(StaffID staffID, StaffFirstName firstName, StaffLastName lastName, StaffEmail email, StaffPhoneNumber phoneNumber, LicenseNumber licenseNumber, bool isActive, AvailabilitySlots availabilitySlots, string specializationID)
    {
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

    /// <summary>
    /// Changes the first name of the staff member.
    /// </summary>
    /// <param name="firstName">The first name to change.</param>
    public void changeFirstName(string firstName)
    {
        this.firstName = new StaffFirstName(firstName);
    }

    /// <summary>
    /// Changes the last name of the staff member.
    /// </summary>
    /// <param name="lastName">The last name to change.</param>
    public void changeLastName(string lastName)
    {
        this.lastName = new StaffLastName(lastName);
    }

    /// <summary>
    /// Changes the email of the staff member.
    /// </summary>
    /// <param name="email">The email to change.</param>
    public void changeEmail(string email)
    {
        this.email = new StaffEmail(email);
    }

    /// <summary>
    /// Changes the phone number of the staff member.
    /// </summary>
    /// <param name="phoneNumber">The phone number to change.</param>
    public void changePhoneNumber(string phoneNumber)
    {
        this.phoneNumber = new StaffPhoneNumber(phoneNumber);
    }
    public void changeSpecializationID(string specializationID)
    {
        this.specializationID = specializationID;
    }

    /// <summary>
    /// Changes the license number of the staff member.
    /// </summary>
    /// <param name="licenseNumber">The license number to change.</param>
    public void changeLicenseNumber(string licenseNumber)
    {
        this.licenseNumber = new LicenseNumber(licenseNumber);
    }

    /// <summary>
    /// Changes the status of the staff member.
    /// </summary>
    /// <param name="isActive">The status to change.</param>
    public void changeActiveStatus(bool isActive)
    {
        this.isActive = isActive;
    }
/* METODO DEIXA DE FAZER SENTIDO COM A IMPLEMENTAÇÃO DO AGREGADO DE AVAILABILITYSLOT
    /// <summary>
    /// Changes the availability slots of the staff member.
    /// </summary>
    /// <param name="availabilitySlots">The availability slots to change.</param>
    public void changeAvailabilitySlots(List<string> availabilitySlotsID)
    {
        AvailabilitySlot availabilitySlots = new AvailabilitySlot();
        this.availabilitySlotsID = availabilitySlotsID;
    }*/

    /// <summary>
    /// Changes the full name of the staff member.
    /// </summary>
    /// <param name="fullName">The full name to change.</param>
    public void changeFullName(StaffFullName fullName)
    {
        this.fullName = fullName;
    }


    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Staff staff = (Staff)obj;
        return staffID == staff.staffID;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return staffID.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current staff member's id.
    /// </summary>
    /// <returns>A string that represents the current staff member's id.</returns>
    public override string ToString()
    {
        return staffID.ToString();
    }
}