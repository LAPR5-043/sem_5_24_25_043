using System;
using Domain.PatientAggregate;
using src.Domain.Shared;
using src.Services.IServices;


namespace src.Services.Services
{
    public class PatientService : IPatientService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IPatientRepository patientRepository;
        private readonly ILogService logService;
        private static string patientDeleteLog1 = "Patient deleted with success;PatientId:";

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository, ILogService logService)
        {
            this.unitOfWork = unitOfWork;
            this.patientRepository = patientRepository;
            this.logService = logService;
        }


        public async Task<bool> DeletePatientAsync(string id)
        {
            var patient = await patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
            if (patient == null)
            {
                return false;
            }
            patientRepository.Remove(patient);
            await unitOfWork.CommitAsync();

            await logService.CreateLogAsync("Patient deleted with success;PatientId:" + id, "colocar@emailtoken.aqui"); 
            return true;
        }

        public async Task<PatientDto> CreatePatientAsync(PatientDto patient)
        {

            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient data is null.");
            }

            Boolean validation = validatePatientInformation(patient.Email, patient.PhoneNumber);

            if (validation)
            {
                throw new Exception("Patient already exists.");
            }

            var newPatient = new Patient();
            newPatient.firstName = new PatientFirstName(patient.FirstName);
            newPatient.lastName = new PatientLastName(patient.LastName);
            newPatient.fullName = new PatientFullName(patient.FirstName, patient.LastName);
            newPatient.email = new PatientEmail(patient.Email);
            newPatient.phoneNumber = new PatientPhoneNumber(patient.PhoneNumber);
            newPatient.emergencyContact = new EmergencyContact(patient.EmergencyContactName, patient.EmergencyContactPhoneNumber);
            newPatient.dateOfBirth = new DateOfBirth(patient.DayOfBirth, patient.MonthOfBirth, patient.YearOfBirth);
            newPatient.gender = GenderExtensions.FromString(patient.Gender);


            await patientRepository.AddAsync(newPatient);
            await unitOfWork.CommitAsync();

            return new PatientDto(newPatient);
        }

        private Boolean validatePatientInformation(string email, string phoneNumber)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("Invalid patient data.");
            }

            if (!email.Contains("@"))
            {
                throw new Exception("Invalid email.");
            }

            if (phoneNumber.Length != 9)
            {
                throw new Exception("Invalid phone number.");
            }

            return patientRepository.PatientExists(email, phoneNumber);
        }

        public Task<PatientDto> GetPatientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}