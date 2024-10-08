public class Staff
{
    public StaffFullName FullName { get; }
    public LicenseNumber License { get; }
    public IsActive ActiveStatus { get; }
    public StaffContactInformation ContactInformation { get; }
    public AvailabilitySlots Availability { get; }
    
    public Staff(StaffFullName fullName, LicenseNumber license, IsActive isActive,
                 StaffPhoneNumber staffPhoneNumber,StaffEmail staffEmail, AvailabilitySlots availabilitySlots)
    {
        FullName = fullName;
        License = license;
        ActiveStatus = isActive;
        ContactInformation = contactInfo;
        Availability = availabilitySlots;
    }


}