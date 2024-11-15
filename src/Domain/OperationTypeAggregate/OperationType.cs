using Azure;
using src.Domain.Shared;

public class OperationType : Entity<OperationTypeName>, IAggregateRoot
{
    public OperationTypeName operationTypeName { get; set; }
    public OperationTypeDescription operationTypeDescription { get; set; }
    public EstimatedDuration estimatedDuration { get; set; }
    public bool isActive { get; set; }
    public Dictionary<string, int> specializations { get; set; }
 
    public OperationType(string operationTypeName,string operationTypeDescription, int anesthesia, int operation,int cleaning, bool isActive, Dictionary<string, int> specialization)
    {
        this.operationTypeName = new OperationTypeName(operationTypeName);
        this.operationTypeDescription = new OperationTypeDescription(operationTypeDescription);
        this.estimatedDuration = new EstimatedDuration( anesthesia,  operation, cleaning);
        this.isActive = isActive;
        this.specializations = specialization;
    }

    public OperationType(OperationTypeName operationTypeName,OperationTypeDescription operationTypeDescription , EstimatedDuration estimatedDuration, bool isActive, Dictionary<string, int> specialization)
    {
        this.operationTypeName = operationTypeName;
        this.operationTypeDescription = operationTypeDescription;
        this.estimatedDuration = estimatedDuration;
        this.isActive = isActive;
        this.specializations = specialization;
    }
    public OperationType()
    {
    }

    public void changeOperationTypeName(string operationTypeName)
    {
        this.operationTypeName = new OperationTypeName(operationTypeName);
    }

    public void changeOperationTypeDescription(string operationTypeDescription)
    {
        this.operationTypeDescription = new OperationTypeDescription(operationTypeDescription);
    }

    public void changeEstimatedDuration(int anesthesia, int operation,int cleaning)
    {
        this.estimatedDuration = new EstimatedDuration(anesthesia, operation, cleaning);
    }

    public void changeActiveStatus(bool isActive)
    {
        this.isActive = isActive;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        OperationType operationType = (OperationType)obj;
        return operationTypeDescription.Equals(operationType.operationTypeDescription) && operationTypeName.Equals(operationType.operationTypeName) && estimatedDuration.Equals(operationType.estimatedDuration);
    }

    public override int GetHashCode()
    {
        return operationTypeName.GetHashCode();
    }

    public override string ToString()
    {
        return operationTypeName.operationTypeName;
    }

    public void changeStatus()
    {
        this.isActive = !this.isActive;
    }
}
