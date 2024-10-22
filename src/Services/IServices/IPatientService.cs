using System;
using Domain.PatientAggregate;
using Microsoft.AspNetCore.Mvc;

namespace src.Services.IServices
{
    public interface IPatientService
    {
        Task<OkObjectResult> GetAllPatientsAsync();
        Task<ActionResult<IEnumerable<PatientDto>>> GetPatientsFilteredAsync(string? firstName, string? lastName, string? email, string? phoneNumber, string? medicalRecordNumber, string? dateOfBirth, string? gender, string? sortBy);
        Task<PatientDto> GetPatientByIdAsync(string id);

        Task<bool> DeletePatientAsync(string id);

        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);

        Task<Patient> GetPatientEntityByIdAsync(string id);
        Task<bool> UpdatePatientAsync(string id, PatientDto patientDto);
        bool AcceptRequests(List<long> requestIds);
        Task RegisterNewPatientIAMAsync(string email, string patientEmail, string password);
    }
}