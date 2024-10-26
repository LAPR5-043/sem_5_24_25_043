using src.Domain.Shared;

namespace Domain.PatientAggregate
{
    public interface IPatientRepository : IRepository<Patient, MedicalRecordNumber>
    {
        Boolean PatientExists(string email, string phoneNumber);
        Boolean PatientExists(string email);
        void UpdateAsync(Patient patient);
        

    }
}