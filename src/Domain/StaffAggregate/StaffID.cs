using src.Domain.Shared;

public class StaffID  : EntityId{

    public string id { get; }

    public StaffID(string id) : base(id) {
        this.id = id;
    }

    public StaffID() : base(null) {
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        StaffID staffID = (StaffID)obj;
        return id == staffID.id;
    }    

    public override int GetHashCode() { 
        return id.GetHashCode();
    }    
    public override string ToString()
    {
        return id;
    }

    protected override object createFromString(string text)
    {
        return new String(text);
    }

    public override string AsString()
    {
        return id;
    }
}