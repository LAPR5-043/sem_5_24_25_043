using src.Domain.Shared;

public class Specialization : Entity<SpecializationName>, IAggregateRoot{

    public SpecializationName SpecializationId { get; private set; }
    public SpecializationDescription description { get; private set; }

    public Specialization(string name, string description){
        this.SpecializationId = new SpecializationName(name);
        this.description = new SpecializationDescription(description);
    }
    public Specialization(){
        this.SpecializationId = new SpecializationName("");
    }
    public string Name(){
        return SpecializationId.ToString();
    }

    public string Description(){
        return description.ToString();
    }        

    public void changeName(string name){
        this.SpecializationId = new SpecializationName(name);
    }

    public void changeDescription(string description){
        this.description = new SpecializationDescription(description);
    }        



}