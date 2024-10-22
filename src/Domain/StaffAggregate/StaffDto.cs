using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a data transfer object for the Staff entity.
/// </summary>
public class StaffDto
{

    /// <summary>
    /// Gets or sets the id of the staff.
    /// </summary>
    public string? StaffID { get; set; }

    /// <summary>
    /// Gets or sets the first name of the staff.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the staff. 
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the full name of the staff.
    /// </summary>
    public string? FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Gets or sets the email of the staff.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the staff.   
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the license number of the staff.
    /// </summary>
    public string? LicenseNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the staff is active.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Gets or sets the list of availability slots of the staff.
    /// </summary>
    public List<String>? AvailabilitySlots { get; set; }

    /// <summary>
    /// Gets or sets the specialization ID of the staff.
    /// </summary>
    public string? SpecializationID { get; set; }

    public StaffDto(Staff staff)
    {

        this.StaffID = staff.staffID.AsString();
        this.FirstName = staff.firstName.ToString();
        this.LastName = staff.lastName.ToString();
        this.Email = staff.email.ToString();
        this.PhoneNumber = staff.phoneNumber.ToString();
        this.LicenseNumber = staff.licenseNumber.ToString();
        this.IsActive = staff.isActive;
        this.AvailabilitySlots = generateAvailabilitySlots(staff.availabilitySlots);
        this.SpecializationID = staff.specializationID;

    }

    [JsonConstructor]
    public StaffDto(string StaffID, string FirstName, string LastName, string Email, string PhoneNumber, string LicenseNumber, bool IsActive, List<String> AvailabilitySlots, string SpecializationID)
    {
        this.StaffID = StaffID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.LicenseNumber = LicenseNumber;
        this.IsActive = IsActive;
        this.AvailabilitySlots = AvailabilitySlots ?? new List<string>();
        this.SpecializationID = SpecializationID;
    }

    private List<String>? generateAvailabilitySlots(AvailabilitySlots availabilitySlots)
    {
        if (availabilitySlots == null)
        {
            return null;
        }
        List<String> slots = new List<String>();
        foreach (var slot in availabilitySlots.slots)
        {
            slots.Add(slot.ToString());
        }
        return slots;

    }
}
