using Domain.OperationRequestAggregate;
using Domain.PatientAggregate;
using Infrastructure.PatientRepository;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Sempi5.Infrastructure.StaffRepository;
using src.Domain;
using src.Domain.AppointmentAggregate;
using src.Domain.PatientAggregate;
using src.Domain.SurgeryRoomAggregate;

namespace src.Models;

public class AppContext : DbContext
{
    public AppContext()
    { }
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Patient> Patients { get; set; }
    
    //public DbSet<Appointment> Appointments { get; set; } = null!;
    //public DbSet<OperationRequest> OperationRequests { get; set; } = null!;
    //public DbSet<OperationType> OperationTypes { get; set; } = null!;
    //public DbSet<Specialization> Specializations { get; set; } = null!;
    //public DbSet<SurgeryRoom> SurgeryRooms { get; set; } = null!;

  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Staff>(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Patient>(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Log>(new LogEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id=new MedicalRecordNumber(1), medicalRecordNumber = new MedicalRecordNumber(1), firstName = new PatientFirstName("John"), lastName = new PatientLastName("Doe"),
                         fullName = new PatientFullName("John","Doe"),email = new PatientEmail("john@email.com"), phoneNumber = new PatientPhoneNumber(919919919),
                          emergencyContact = new EmergencyContact("Jane", 919919919), dateOfBirth = new DateOfBirth("01", "01", "1999"), gender = Gender.Male,
                         allergiesAndConditions = new List<AllergiesAndConditions>(), appointmentHistory = new AppointmentHistory() },
            new Patient {Id=new MedicalRecordNumber(2), medicalRecordNumber = new MedicalRecordNumber(2), firstName = new PatientFirstName("Jane"), lastName = new PatientLastName("Does"),
                         fullName = new PatientFullName("Jane","Does"),email = new PatientEmail("Jane@email.com"), phoneNumber = new PatientPhoneNumber(919991919),
                          emergencyContact = new EmergencyContact("Jane", 919999119), dateOfBirth = new DateOfBirth("01", "01", "1999"), gender = Gender.Male,
                         allergiesAndConditions = new List<AllergiesAndConditions>(), appointmentHistory = new AppointmentHistory() }            
        );

    }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer( "Server=vsgate-s1.dei.isep.ipp.pt,11366;Database=MedOptima;User Id=MedOptima;Password=Medoptima;MultipleActiveResultSets=true;TrustServerCertificate=True", sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        }
    }

}