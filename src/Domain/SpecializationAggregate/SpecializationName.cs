using System.Reflection;
using System.Runtime.Serialization;
using src.Domain.Shared;

public class SpecializationName : EntityId {

    public string name { get;}


    public SpecializationName(string name) : base(name) {
        if (string.IsNullOrEmpty(name)){
            throw new ArgumentException("Description cannot be empty.");
        }
        this.name = name;
    }    

    public SpecializationName() : base(null ){}



    public override string ToString(){
        return name;
    }

    protected override object createFromString(string text)
    {

        return text;
    }

    public override string AsString()
    {
        return name;
    }
}