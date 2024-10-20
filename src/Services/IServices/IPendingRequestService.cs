using System;

namespace src.Services.IServices
{
    public interface IPendingRequestService
    {
        PendingRequest AddPendingRequest(string userID, string oldValue, string newValue, string type);

        
        PendingRequest GetByIdAsync(LongId id);
        
    }

    
}