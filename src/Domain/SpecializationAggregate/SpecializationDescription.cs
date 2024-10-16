using src.Domain.Shared;

public class SpecializationDescription : IValueObject {

    public string description { get;}

    public SpecializationDescription(string description){
        if (string.IsNullOrEmpty(description)){
            throw new ArgumentException("Description cannot be empty.");
        }
        this.description = description;
    }    

    public SpecializationDescription(){}

    public override string ToString(){
        return description;
    }    

}