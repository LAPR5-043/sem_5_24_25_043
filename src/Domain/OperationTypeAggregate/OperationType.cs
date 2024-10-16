using NuGet.Protocol.Plugins;
using src.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OperationType  : Entity <OperationTypeID>{
    public OperationTypeID operationTypeID { get; private set; }
 
    public IsActive isActive { get; private set;}

    public OperationTypeName operationType { get; private set; }

    public EstimatedDuration estimatedDuration { get; private set; }


    public OperationType(long operationTypeID, bool isActive, string operationType, int hours, int minutes){
        //this.operationTypeID = new OperationTypeID(operationTypeID);
        this.isActive = new IsActive(isActive);
        this.operationType = new OperationTypeName(operationType);
        this.estimatedDuration = new EstimatedDuration(hours, minutes);
    }
    public OperationType(){}
    public long operationTypeId(){
        return operationTypeID.GetId();
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

    public void changeOperationType(string operationType){
        this.operationType = new OperationTypeName(operationType);
    }

    public void changeOperationDuration(int hours, int minutes){
        this.estimatedDuration = new EstimatedDuration(hours, minutes);
    }

    public void changeOperationStatus(bool isActive){
        this.isActive = new IsActive(isActive);
    }






}
