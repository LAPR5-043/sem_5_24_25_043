using src.Domain.Shared;

public class SpecializationName : EntityId {

    private string name { get;}

    public SpecializationName(string name) : base(name) {
        if (string.IsNullOrEmpty(name)){
            throw new ArgumentException("Description cannot be empty.");
        }
        this.name = name;
    }    

    public override string ToString(){
        return name;
    }

    protected override object createFromString(string text)
    {
        throw new NotImplementedException();
    }

    public override string AsString()
    {
        return name;
    }
}