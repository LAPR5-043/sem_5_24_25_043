using src.Domain.Shared;

public class OperationType : Entity<OperationTypeName>, IAggregateRoot
{
    public OperationTypeName operationTypeName { get; set; }
    public EstimatedDuration estimatedDuration { get; set; }
    public bool isActive { get; set; }
    public Dictionary<string, int> specializations { get; set; }

    public OperationType(string operationTypeName, int hours, int minutes, bool isActive, Dictionary<string, int> specialization)
    {
        this.operationTypeName = new OperationTypeName(operationTypeName);
        this.estimatedDuration = new EstimatedDuration(hours, minutes);
        this.isActive = isActive;
        this.specializations = specialization;
    }

    public OperationType(OperationTypeName operationTypeName, EstimatedDuration estimatedDuration, bool isActive, Dictionary<string, int> specialization)
    {
        this.operationTypeName = operationTypeName;
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

    public void changeEstimatedDuration(int hours, int minutes)
    {
        this.estimatedDuration = new EstimatedDuration(hours, minutes);
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
        return operationTypeName.Equals(operationType.operationTypeName) && estimatedDuration.Equals(operationType.estimatedDuration);
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
