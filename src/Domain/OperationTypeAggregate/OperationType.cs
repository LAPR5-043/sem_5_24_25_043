using NuGet.Protocol.Plugins;

public class OperationType  {

    private OperationTypeID operationTypeID { get; set; }
    private IsActive isActive { get; set;}
    private OperationTypeName operationType { get; set; }
    private EstimatedDuration estimatedDuration { get; set; }


    public OperationType(long operationTypeID, bool isActive, string operationType, int hours, int minutes){
        this.operationTypeID = new OperationTypeID(operationTypeID);
        this.isActive = new IsActive(isActive);
        this.operationType = new OperationTypeName(operationType);
        this.estimatedDuration = new EstimatedDuration(hours, minutes);
    }

    public long operationTypeId(){
        return operationTypeID.getID();
    }

    public bool isOperationActive(){
        return isActive.isActive();
    }    

    public string operationTypeName(){
        return operationType.getName();
    }

    public string operationDuration(){
        return estimatedDuration.getHours() + ":" + estimatedDuration.getMinutes() ;
    }


}
