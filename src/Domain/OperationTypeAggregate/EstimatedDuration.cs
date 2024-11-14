using src.Domain.Shared;

public class EstimatedDuration : IValueObject
{
    public int anesthesia { get; }
    public int operation { get; }
    public int cleaning { get; }

    public EstimatedDuration(int anesthesia, int operation,int cleaning)
    {
        this.anesthesia = anesthesia;
        this.operation = operation;
        this.cleaning = cleaning;
    }

    public EstimatedDuration()
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        EstimatedDuration estimatedDuration = (EstimatedDuration)obj;
        return anesthesia == estimatedDuration.anesthesia && operation == estimatedDuration.operation && cleaning == estimatedDuration.cleaning;
    }

    public override int GetHashCode() {
        return anesthesia.GetHashCode() + operation.GetHashCode() + cleaning.GetHashCode();
    }

    public override string ToString()
    {
        return  "anesthesia:"+ anesthesia + ",operation:" + operation + ",cleaning:" + cleaning;
    }

}
