using src.Domain.Shared;

public class PendingRequest : Entity<LongId> {

    public LongId requestID { get;  set;}
    public string userId { get;  set;}
    public string attributeName { get;  set;}
    public string pendingValue { get;  set;}
    public string oldValue { get;  set;}

    /*
    public PendingRequest(string requestID, string userId, string attributeName, string pendingValue, string oldValue)
    {
        this.requestID = requestID;
        this.userId = userId;
        this.attributeName = attributeName;
        this.pendingValue = pendingValue;
        this.oldValue = oldValue;
    }*/

    public PendingRequest()
    {
    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        PendingRequest pendingRequest = (PendingRequest)obj;
        return this.requestID.Value == pendingRequest.requestID.Value;  
    }

    public override int GetHashCode() {
        return requestID.GetHashCode();
    }
}