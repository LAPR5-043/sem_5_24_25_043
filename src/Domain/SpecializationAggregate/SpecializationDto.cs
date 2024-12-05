    public class SpecializationDto
    {
        public string? SpecializationName { get; set; }
        public string? SpecializationDescription { get; set; }

        public SpecializationDto()
        {
        }

        public SpecializationDto(string specializationName, string specializationDescription)
        {
            SpecializationName = specializationName;
            SpecializationDescription = specializationDescription;
        }

        public SpecializationDto(Specialization specialization)
        {
            SpecializationName = specialization.specializationName.specializationName;
            SpecializationDescription = specialization.specializationDescription.specializationDescription;
        }
    }