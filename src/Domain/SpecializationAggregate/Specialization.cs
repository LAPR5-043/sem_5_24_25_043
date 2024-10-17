using System.ComponentModel.DataAnnotations;
using src.Domain.Shared;

public class Specialization : Entity<SpecializationName>, IAggregateRoot {
    [Key]
    public SpecializationName specializationName { get; private set; }
    [Required]
    public SpecializationDescription specializationDescription { get; private set;}

    public Specialization(string specializationName, string specializationDescription){
        this.specializationName = new SpecializationName(specializationName);
        this.specializationDescription = new SpecializationDescription(specializationDescription);
    }

    public Specialization(SpecializationName specializationName, SpecializationDescription specializationDescription){
        this.specializationName = specializationName;
        this.specializationDescription = specializationDescription;
    }

    public Specialization() {
    
    }


    public void changeSpecializationName(string specializationName)
    {
        this.specializationName = new SpecializationName(specializationName);
    }

    public void changeSpecializationDescription(string specializationDescription)
    {
        this.specializationDescription = new SpecializationDescription(specializationDescription);
    }


    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Specialization specialization = (Specialization)obj;
        return specializationName.Equals(specialization.specializationName) && specializationDescription.Equals(specialization.specializationDescription);
    }

    public override int GetHashCode() {
        return specializationName.GetHashCode();
    }

    public override string ToString()
    {
        return specializationName.AsString();
    }
}


