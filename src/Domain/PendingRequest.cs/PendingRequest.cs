public class PendingRequest {

    public string requestID { get; private set;}
    public string userId { get; private set;}
    public string attributeName { get; private set;}
    public string pendingValue { get; private set;}
    public string oldValue { get; private set;}


    public PendingRequest(string requestID, string userId, string attributeName, string pendingValue, string oldValue)
    {
        this.requestID = requestID;
        this.userId = userId;
        this.attributeName = attributeName;
        this.pendingValue = pendingValue;
        this.oldValue = oldValue;
    }

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
        return this.requestID == pendingRequest.requestID;  
    }

    public override int GetHashCode() {
        return requestID.GetHashCode();
    }
}