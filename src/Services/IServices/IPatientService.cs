using System;
using Domain.PatientAggregate;

namespace src.Services.IServices
{
    public interface IPatientService
    {
        Task<bool> DeletePatientAsync(string id);

        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);

        Task<PatientDto> GetPatientByIdAsync(string id);

        Task<Patient> GetPatientEntityByIdAsync(string id);
        Task<bool> UpdatePatientAsync(string id, PatientDto patientDto);
        bool AcceptRequests(List<long> requestIds);
        Task RegisterNewPatientIAMAsync(string email, string patientEmail, string password);
    }
}