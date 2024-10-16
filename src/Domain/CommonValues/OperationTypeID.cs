using src.Domain.Shared;

public class OperationTypeID : EntityId {
    private long Id { get; set;}

    public OperationTypeID(long value) : base(value) {
        SetId(value);
    }

    public long GetId(){
        return Id;
    }


    private void SetId(long id){
        if (id < 0) {
            throw new ArgumentException("ID cannot be negative.");
        }
        this.Id = id;
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