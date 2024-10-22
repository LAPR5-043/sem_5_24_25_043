using src.Domain.Shared;

public class SpecializationName : EntityId  
{
    public string specializationName { get; }
    
    public SpecializationName(string specializationName) : base(specializationName)
    {
        if (string.IsNullOrEmpty(specializationName))
        {
            throw new System.ArgumentException("Specialization Name cannot be null or empty");
        }
        this.specializationName = specializationName;
    }
    public SpecializationName() : base (null)
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        SpecializationName specializationName = (SpecializationName)obj;
        return this.specializationName == specializationName.specializationName;
    }

    public override int GetHashCode() {
        return specializationName.GetHashCode();
    }
    
    public override string ToString()
    {
        return specializationName;
    }

    protected override object createFromString(string text)
    {
        return new String(text);
    }

    public override string AsString()
    {
        return specializationName;
    }
}
