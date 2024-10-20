using System;

namespace src.Services.IServices
{
    public interface IOperationRequestService
    {
        Task<bool> DeleteOperationRequestAsync(int id);
    }
}