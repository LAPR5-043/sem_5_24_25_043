using System;
using Domain.PatientAggregate;

namespace src.Services.IServices
{
    public interface IPatientService
    {
        Task<bool> DeletePatientAsync(string id);

        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);

        Task<PatientDto> GetPatientByIdAsync(int id);

        Task<Patient> GetPatientEntityByIdAsync(int id);
        Task<bool> UpdatePatientAsync(int id, PatientDto patientDto);
        bool AcceptRequests(List<long> requestIds);
    }
}