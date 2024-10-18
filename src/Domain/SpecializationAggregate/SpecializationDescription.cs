using src.Domain.Shared;

public class SpecializationDescription : IValueObject
{
    public string specializationDescription { get; }
    
    public SpecializationDescription(string specializationDescription)
    {
        this.specializationDescription = specializationDescription;
    }
    public SpecializationDescription()
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        SpecializationDescription specializationDescription = (SpecializationDescription)obj;
        return this.specializationDescription == specializationDescription.specializationDescription;
    }

    public override int GetHashCode() {
        return specializationDescription.GetHashCode();
    }

    public override string ToString()
    {
        return specializationDescription;
    }

}