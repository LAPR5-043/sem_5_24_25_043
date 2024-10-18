using Domain.PatientAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


public class PatientRepository : BaseRepository<Patient, MedicalRecordNumber>, IPatientRepository
{
    private readonly AppContext context;

    public PatientRepository(AppContext context) : base(context.Patients)
    {
        this.context = context;
    }
}