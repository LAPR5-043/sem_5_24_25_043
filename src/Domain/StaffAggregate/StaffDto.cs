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
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the list of availability slots of the staff.
    /// </summary>
    public List<String>? AvailabilitySlots { get; set; }

    /// <summary>
    /// Gets or sets the specialization ID of the staff.
    /// </summary>
    public string? SpecializationID { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaffDto"/> class.
    /// </summary>
    public StaffDto(Staff staff)
    {

        if (staff == null)
        {
            throw new ArgumentNullException(nameof(staff), "Staff cannot be null.");
        }

        StaffID = staff.staffID?.ToString() ?? string.Empty;
        FirstName = staff.firstName?.ToString() ?? string.Empty;
        LastName = staff.lastName?.ToString() ?? string.Empty;
        Email = staff.email?.ToString() ?? string.Empty;
        PhoneNumber = staff.phoneNumber?.ToString() ?? string.Empty;
        LicenseNumber = staff.licenseNumber?.ToString() ?? string.Empty;
        IsActive = staff.isActive ? staff.isActive : false;
        AvailabilitySlots = generateAvailabilitySlots(staff.availabilitySlots);
        SpecializationID = staff.specializationID ?? string.Empty;

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaffDto"/> class.
    /// </summary>
    /// <param name="StaffID"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Email"></param>
    /// <param name="PhoneNumber"></param>
    /// <param name="LicenseNumber"></param>
    /// <param name="IsActive"></param>
    /// <param name="AvailabilitySlots"></param>
    /// <param name="SpecializationID"></param>
    [JsonConstructor]
    public StaffDto(    string? StaffID, string? FirstName, string? LastName, string? FullName, string? Email, string? PhoneNumber, string? LicenseNumber, bool? IsActive, List<String> AvailabilitySlots, string? SpecializationID)
    {
        this.StaffID = StaffID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.LicenseNumber = LicenseNumber;
        this.IsActive = IsActive ?? false;
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
