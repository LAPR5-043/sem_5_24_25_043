using System;

namespace src.Services.IServices
{
    public interface IPendingRequestService
    {
        Task<PendingRequest> AddPendingRequestAsync(string userID, string oldValue, string newValue, string type);

        
        PendingRequest GetByIdAsync(LongId id);
        
    }

    
}