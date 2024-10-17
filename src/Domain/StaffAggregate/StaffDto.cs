using System.ComponentModel.DataAnnotations;

public class StaffDto
{

    public string StaffID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string LicenseNumber { get; set; }

    public bool IsActive { get; set; }

    public List<String> AvailabilitySlots { get; set; }
    public string SpecializationID { get; set; }

    public StaffDto(Staff staff){

        this.StaffID = staff.staffID.AsString();
        this.FirstName = staff.firstName.ToString();
        this.LastName = staff.lastName.ToString();
        this.FullName = staff.fullName.ToString();
        this.Email = staff.email.ToString();
        this.PhoneNumber = staff.phoneNumber.ToString();
        this.LicenseNumber = staff.licenseNumber.ToString();
        this.IsActive = staff.isActive;
        this.AvailabilitySlots = generateAvailabilitySlots(staff.availabilitySlots);
        this.SpecializationID = staff.specializationID;

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
