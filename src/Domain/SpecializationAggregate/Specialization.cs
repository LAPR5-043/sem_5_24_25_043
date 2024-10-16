using src.Domain.Shared;

public class Specialization : Entity<SpecializationName>, IAggregateRoot{

    private SpecializationName name { get; set; }
    private SpecializationDescription description { get; set; }

    public Specialization(string name, string description){
        this.name = new SpecializationName(name);
        this.description = new SpecializationDescription(description);
    }

    public string Name(){
        return name.AsString();
    }

    public string Description(){
        return description.ToString();
    }        

    public void changeName(string name){
        this.name = new SpecializationName(name);
    }

    public void changeDescription(string description){
        this.description = new SpecializationDescription(description);
    }        



}