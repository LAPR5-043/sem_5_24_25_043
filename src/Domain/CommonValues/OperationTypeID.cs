using src.Domain.Shared;

public class OperationTypeID : EntityId { 
    private long id { get; set;}

    public OperationTypeID(long id) : base(id){
        setID(id);
    }

    public long getID(){
        return id;
    }


    private void setID(long id){
        if (id < 0) {
            throw new ArgumentException("ID cannot be negative.");
        }
        this.id = id;
    }

    protected override object createFromString(string text)
    {
        throw new NotImplementedException();
    }

    public override string AsString()
    {
        throw new NotImplementedException();
    }
}