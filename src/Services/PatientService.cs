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

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
        {
            this.unitOfWork = unitOfWork;
            this.patientRepository = patientRepository;
        }
        
    }
}