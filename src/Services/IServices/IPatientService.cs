using System;
using Domain.PatientAggregate;

namespace src.Services.IServices
{
    public interface IPatientService
    {
        Task<bool> DeletePatientAsync(string id);

        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);

        Task<PatientDto> GetPatientByIdAsync(int id);

    }
}