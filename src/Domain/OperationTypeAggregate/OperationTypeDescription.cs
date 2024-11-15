using src.Domain.Shared;

public class OperationTypeDescription  
{
    public string operationTypeDescription  { get; private set; }

    public OperationTypeDescription(string operationTypeDescription )
    {
        this.operationTypeDescription  = operationTypeDescription ;
    }

    public OperationTypeDescription() 
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        OperationTypeDescription operationTypeName = (OperationTypeDescription)obj;
        return this.operationTypeDescription == operationTypeName.operationTypeDescription;
    }

    public override int GetHashCode() {
        return operationTypeDescription.GetHashCode();
    }

    public override string ToString()
    {
        return operationTypeDescription;
    }



}

