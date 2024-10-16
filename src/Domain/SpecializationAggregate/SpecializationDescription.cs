using src.Domain.Shared;

public class SpecializationDescription : IValueObject {

    private string description { get;}

    public SpecializationDescription(string description){
        if (string.IsNullOrEmpty(description)){
            throw new ArgumentException("Description cannot be empty.");
        }
        this.description = description;
    }    

    public override string ToString(){
        return description;
    }    

}