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


        public async Task<bool> DeletePatientAsync(int id)
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
        
    }
}