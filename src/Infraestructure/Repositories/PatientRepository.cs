using Domain.PatientAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;

/// <summary>
/// Patient repository
/// </summary>
public class PatientRepository : BaseRepository<Patient, MedicalRecordNumber>, IPatientRepository
{
    /// <summary>
    /// App context
    /// </summary>
    private readonly AppContext context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public PatientRepository(AppContext context) : base(context.Patients)
    {
        this.context = context;
    }

    /// <summary>
    /// Check if patient exists
    /// </summary>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public bool PatientExists(string email, string phoneNumber)
    {
        return context.Patients
            .AsEnumerable()
            .Any(p => p.Email.Value == email || p.PhoneNumber.Value == phoneNumber);
    }
    
    /// <summary>
    /// Check if patient exists
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public bool PatientExists(string email)
    {
        return context.Patients
            .AsEnumerable()
            .Any(p => p.Email.Value == email);
    }
}