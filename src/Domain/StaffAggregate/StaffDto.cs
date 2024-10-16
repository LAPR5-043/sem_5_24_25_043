public class StaffDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsActive { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }

        public string SpecializationName { get; set; } // Assuming you want to include the name of the specialization

        // Constructor
        public StaffDto(string id, string firstName, string lastName, string licenseNumber, bool isActive, string phoneNumber,string email, string specializationName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
            IsActive = isActive;
            this.phoneNumber = phoneNumber;
            this.email = email;
            SpecializationName = specializationName;
        }

        public StaffDto(Staff staff) {
            Id = staff.License();
            FirstName = staff.FirstName();
            LastName = staff.LastName();
            LicenseNumber = staff.License();
            IsActive = staff.IsActive();
            phoneNumber = staff.PhoneNumber();
            email = staff.Email();
            SpecializationName = staff.Specialization();



        }
    }