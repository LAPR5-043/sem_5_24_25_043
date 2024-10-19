using System;

namespace src.Services.IServices
{
    public interface IPatientService
    {
        Task<bool> DeletePatientAsync(int id);
    }
}