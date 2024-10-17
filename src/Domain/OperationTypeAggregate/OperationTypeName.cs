using src.Domain.Shared;

public class OperationTypeName : EntityId 
{
    public string operationTypeName { get; private set; }

    public OperationTypeName(string operationTypeName) : base(operationTypeName)
    {
        this.operationTypeName = operationTypeName;
    }

    public OperationTypeName() : base (null)
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        OperationTypeName operationTypeName = (OperationTypeName)obj;
        return this.operationTypeName == operationTypeName.operationTypeName;
    }

    public override int GetHashCode() {
        return operationTypeName.GetHashCode();
    }

    public override string ToString()
    {
        return operationTypeName;
    }

    protected override object createFromString(string text)
    {
        return new String(text);
    }

    public override string AsString()
    {
        return operationTypeName;
    }

}

