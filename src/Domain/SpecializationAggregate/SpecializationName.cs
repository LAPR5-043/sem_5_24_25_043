using src.Domain.Shared;
using Microsoft.EntityFrameworkCore;
[Owned]
public class SpecializationName : EntityId  
{
    public string specializationName { get; }
    
    public SpecializationName(string specializationName) : base(specializationName)
    {
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
